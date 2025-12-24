using CarRental.Application.Contracts;
using CarRental.Application.Contracts.Dto;
using CarRental.Application.Contracts.Dto.Analytics;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Host.Controllers;

/// <summary>
/// Analytics endpoints over rentals, cars, and clients.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AnalyticsController(IAnalyticsService analyticsService, ILogger<AnalyticsController> logger) : ControllerBase
{
    /// <summary>
    /// Gets clients who rented cars of the specified model.
    /// </summary>
    /// <param name="modelName">Model name filter.</param>
    [HttpGet("clients-by-model/{modelName}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IEnumerable<ClientDto>>> GetClientsByModel(string modelName)
    {
        logger.LogInformation("{method} called with model {model}", nameof(GetClientsByModel), modelName);
        try
        {
            var result = await analyticsService.GetClientsByModelAsync(modelName);
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "{method} failed", nameof(GetClientsByModel));
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// Gets cars currently rented at the given reference date.
    /// </summary>
    /// <param name="referenceDate">Point-in-time to check.</param>
    [HttpGet("currently-rented")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IEnumerable<CarDto>>> GetCurrentlyRented([FromQuery] DateTime referenceDate)
    {
        logger.LogInformation("{method} called with date {date}", nameof(GetCurrentlyRented), referenceDate);
        try
        {
            var result = await analyticsService.GetCurrentlyRentedCarsAsync(referenceDate);
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "{method} failed", nameof(GetCurrentlyRented));
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// Returns top N cars by rental count.
    /// </summary>
    /// <param name="count">Number of cars to return.</param>
    [HttpGet("top-cars")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IEnumerable<CarRentalCountDto>>> GetTopCars([FromQuery] int count)
    {
        logger.LogInformation("{method} called with count {count}", nameof(GetTopCars), count);
        try
        {
            var result = await analyticsService.GetTopMostRentedCarsAsync(count);
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "{method} failed", nameof(GetTopCars));
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// Returns rental count for each car.
    /// </summary>
    [HttpGet("rental-count-per-car")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IEnumerable<CarRentalCountDto>>> GetRentalCountPerCar()
    {
        logger.LogInformation("{method} called", nameof(GetRentalCountPerCar));
        try
        {
            var result = await analyticsService.GetRentalCountPerCarAsync();
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "{method} failed", nameof(GetRentalCountPerCar));
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// Returns top N clients by total spending.
    /// </summary>
    /// <param name="count">Number of clients to return.</param>
    [HttpGet("top-clients")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IEnumerable<ClientTotalSpentDto>>> GetTopClients([FromQuery] int count)
    {
        logger.LogInformation("{method} called with count {count}", nameof(GetTopClients), count);
        try
        {
            var result = await analyticsService.GetTopClientsBySpendingAsync(count);
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "{method} failed", nameof(GetTopClients));
            return StatusCode(500, "Internal server error");
        }
    }
}
