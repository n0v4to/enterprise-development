using CarRental.Application.Contracts.Generator;
using CarRental.Generator.RabbitMq.Host.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Generator.RabbitMq.Host.Controllers;

/// <summary>
/// Controller for generating and publishing rental contracts.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class GeneratorController(
    RentalGenerator generator,
    RabbitMqProducer producer,
    ILogger<GeneratorController> logger) : ControllerBase
{
    /// <summary>
    /// Generates and publishes rental contracts to RabbitMQ.
    /// </summary>
    /// <param name="count">Number of contracts to generate (default: 10).</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The result of the generation process.</returns>
    [HttpPost("generate")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<GenerationResult>> Generate(
        [FromQuery] int count = 10,
        CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Generating {Count} rental contracts", count);

        try
        {
            var rentals = generator.Generate(count);
            var publishedCount = await producer.PublishBatchAsync(rentals, cancellationToken);

            logger.LogInformation("Successfully published {Published} of {Total} rental contracts", publishedCount, count);

            return Ok(new GenerationResult
            {
                RequestedCount = count,
                PublishedCount = publishedCount,
                Message = $"Successfully published {publishedCount} of {count} rental contracts"
            });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to generate rental contracts");
            return StatusCode(500, "Failed to generate rental contracts");
        }
    }
}