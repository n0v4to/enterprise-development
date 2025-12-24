using System.Text;
using System.Text.Json;
using CarRental.Application.Contracts;
using CarRental.Application.Contracts.Dto;
using CarRental.Infrastructure.RabbitMq.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace CarRental.Infrastructure.RabbitMq.Services;

/// <summary>
/// Background service that consumes rental contract messages from RabbitMQ
/// </summary>
public class RabbitMqConsumer(
    IConnection connection,
    IServiceScopeFactory scopeFactory,
    IOptions<RabbitMqOptions> options,
    ILogger<RabbitMqConsumer> logger) : BackgroundService
{
    private IChannel? _channel;

    private readonly ResiliencePipeline _retryPipeline = new ResiliencePipelineBuilder()
            .AddRetry(new RetryStrategyOptions
            {
                MaxRetryAttempts = options.Value.RetryCount,
                Delay = TimeSpan.FromMilliseconds(options.Value.RetryDelayMs),
                BackoffType = DelayBackoffType.Exponential,
                OnRetry = args =>
                {
                    logger.LogWarning(
                        "Retry attempt {AttemptNumber} for RabbitMQ connection. Waiting {Delay}ms before next attempt",
                        args.AttemptNumber,
                        args.RetryDelay.TotalMilliseconds);
                    return ValueTask.CompletedTask;
                }
            })
            .Build();

    /// <summary>
    /// Executes the background service operation.
    /// </summary>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _retryPipeline.ExecuteAsync(async ct =>
        {
            await InitializeChannelAsync(ct);
            await StartConsumingAsync(ct);
        }, stoppingToken);

        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken);
        }
    }

    /// <summary>
    /// Initializes the RabbitMQ channel and declares the queue.
    /// </summary>
    private async Task InitializeChannelAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Initializing RabbitMQ channel for queue {QueueName}", options.Value.QueueName);

        _channel = await connection.CreateChannelAsync(cancellationToken: cancellationToken);

        await _channel.QueueDeclareAsync(
            queue: options.Value.QueueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null,
            cancellationToken: cancellationToken);

        logger.LogInformation("RabbitMQ channel initialized successfully");
    }

    /// <summary>
    /// Starts consuming messages from the queue.
    /// </summary>
    private async Task StartConsumingAsync(CancellationToken cancellationToken)
    {
        if (_channel == null)
        {
            throw new InvalidOperationException("Channel is not initialized");
        }

        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.ReceivedAsync += async (_, ea) =>
        {
            await ProcessMessageAsync(ea);
        };

        await _channel.BasicConsumeAsync(
            queue: options.Value.QueueName,
            autoAck: false,
            consumer: consumer,
            cancellationToken: cancellationToken);

        logger.LogInformation("Started consuming messages from queue {QueueName}", options.Value.QueueName);
    }

    /// <summary>
    /// Processes a single message received from the queue.
    /// </summary>
    private async Task ProcessMessageAsync(BasicDeliverEventArgs ea)
    {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);

        logger.LogDebug("Received message: {Message}", message);

        try
        {
            var rentalDto = JsonSerializer.Deserialize<RentalCreateUpdateDto>(message);
            if (rentalDto != null)
            {
                using var scope = scopeFactory.CreateScope();
                var rentalService = scope.ServiceProvider
                    .GetRequiredService<IApplicationService<RentalDto, RentalCreateUpdateDto>>();

                await rentalService.CreateAsync(rentalDto);

                logger.LogInformation("Created rental ClientId={ClientId}", rentalDto.ClientId);

                await _channel!.BasicAckAsync(ea.DeliveryTag, multiple: false);
            }
            else
            {
                logger.LogWarning("Null DTO received, Acking to remove from queue");
                await _channel!.BasicAckAsync(ea.DeliveryTag, multiple: false);
            }
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogWarning(
                "Skipping rental creation due to missing entity: {Message}",
                ex.Message);
        }
        catch (JsonException ex)
        {
            logger.LogError(ex, "Failed to deserialize rental message: {Message}", message);
            await _channel!.BasicAckAsync(ea.DeliveryTag, multiple: false);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error processing rental message");
            await _channel!.BasicNackAsync(ea.DeliveryTag, multiple: false, requeue: true);
            await Task.Delay(1000);
        }
    }

    /// <summary>
    /// Stops the service and closes the channel.
    /// </summary>
    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Stopping RabbitMQ consumer");

        if (_channel != null)
        {
            await _channel.CloseAsync(cancellationToken);
            _channel.Dispose();
        }

        await base.StopAsync(cancellationToken);
    }
}