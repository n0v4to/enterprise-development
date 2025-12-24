using System.Text;
using System.Text.Json;
using CarRental.Application.Contracts.Dto;
using CarRental.Generator.RabbitMq.Host.Options;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;

namespace CarRental.Generator.RabbitMq.Host.Services;

/// <summary>
/// Service for publishing rental contracts to RabbitMQ
/// </summary>
public class RabbitMqProducer(
    IConnection connection,
    IOptions<RabbitMqOptions> options,
    ILogger<RabbitMqProducer> logger)
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
                        "Retry attempt {AttemptNumber} for RabbitMQ publish. Waiting {Delay}ms before next attempt",
                        args.AttemptNumber,
                        args.RetryDelay.TotalMilliseconds);
                    return ValueTask.CompletedTask;
                }
            })
            .Build();

    /// <summary>
    /// Publishes a single rental contract to the queue.
    /// </summary>
    /// <param name="rental">The rental contract DTO.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    public async Task PublishAsync(RentalCreateUpdateDto rental, CancellationToken cancellationToken = default)
    {
        await _retryPipeline.ExecuteAsync(async ct =>
        {
            await EnsureChannelAsync(ct);

            var message = JsonSerializer.Serialize(rental);
            var body = Encoding.UTF8.GetBytes(message);

            var properties = new BasicProperties
            {
                Persistent = true,
                ContentType = "application/json"
            };

            await _channel!.BasicPublishAsync(
                exchange: string.Empty,
                routingKey: options.Value.QueueName,
                mandatory: false,
                basicProperties: properties,
                body: body,
                cancellationToken: ct);

            logger.LogDebug("Published rental contract: ClientId={ClientId}, CarId={CarId}", rental.ClientId, rental.CarId);
        }, cancellationToken);
    }

    /// <summary>
    /// Publishes multiple rental contracts to the queue
    /// </summary>
    /// <param name="rentals">Rental contracts to publish</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Number of successfully published contracts</returns>
    public async Task<int> PublishBatchAsync(
        IEnumerable<RentalCreateUpdateDto> rentals,
        CancellationToken cancellationToken = default)
    {
        var count = 0;
        foreach (var rental in rentals)
        {
            try
            {
                await PublishAsync(rental, cancellationToken);
                count++;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to publish rental contract");
            }
        }
        return count;
    }

    /// <summary>
    /// Ensures that the RabbitMQ channel is created and open.
    /// </summary>
    private async Task EnsureChannelAsync(CancellationToken cancellationToken)
    {
        if (_channel != null && _channel.IsOpen)
            return;

        logger.LogInformation("Creating RabbitMQ channel for queue {QueueName}", options.Value.QueueName);

        _channel = await connection.CreateChannelAsync(cancellationToken: cancellationToken);

        await _channel.QueueDeclareAsync(
            queue: options.Value.QueueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null,
            cancellationToken: cancellationToken);

        logger.LogInformation("RabbitMQ channel created successfully");
    }
}