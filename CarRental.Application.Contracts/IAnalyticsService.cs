using CarRental.Application.Contracts.Dto;
using CarRental.Application.Contracts.Dto.Analytics;

namespace CarRental.Application.Contracts;

/// <summary>
/// Service interface for analytics operations on car rental data.
/// </summary>
public interface IAnalyticsService
{
    /// <summary>
    /// Gets all clients who rented a specific car model, ordered by full name.
    /// </summary>
    /// <param name="modelName">The name of the car model.</param>
    /// <returns>A collection of clients who rented cars of the specified model.</returns>
    public Task<IEnumerable<ClientDto>> GetClientsByModelAsync(string modelName);

    /// <summary>
    /// Gets all cars that are currently rented at the specified date and time.
    /// </summary>
    /// <param name="referenceDate">The date and time to check for active rentals.</param>
    /// <returns>A collection of cars that are currently rented.</returns>
    public Task<IEnumerable<CarDto>> GetCurrentlyRentedCarsAsync(DateTime referenceDate);

    /// <summary>
    /// Gets the top N most frequently rented cars.
    /// </summary>
    /// <param name="count">The number of top cars to return.</param>
    /// <returns>A collection of the most frequently rented cars with rental counts.</returns>
    public Task<IEnumerable<CarRentalCountDto>> GetTopMostRentedCarsAsync(int count);

    /// <summary>
    /// Gets the rental count for each car.
    /// </summary>
    /// <returns>A collection of car rental counts.</returns>
    public Task<IEnumerable<CarRentalCountDto>> GetRentalCountPerCarAsync();

    /// <summary>
    /// Gets the top N clients by total amount spent on rentals.
    /// </summary>
    /// <param name="count">The number of top clients to return.</param>
    /// <returns>A collection of clients with their total spending.</returns>
    public Task<IEnumerable<ClientTotalSpentDto>> GetTopClientsBySpendingAsync(int count);
}