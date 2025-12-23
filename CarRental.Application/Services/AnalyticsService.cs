using AutoMapper;
using CarRental.Application.Contracts;
using CarRental.Application.Contracts.Dto;
using CarRental.Application.Contracts.Dto.Analytics;
using CarRental.Domain;
using CarRental.Domain.Models;

namespace CarRental.Application.Services;

/// <summary>
/// Service providing analytics queries based on rentals and related entities.
/// </summary>
public class AnalyticsService(
    IRepository<Rental> rentalRepository,
    IRepository<Client> clientRepository,
    IRepository<Car> carRepository,
    IRepository<ModelGeneration> generationRepository,
    IRepository<Model> modelRepository,
    IMapper mapper) : IAnalyticsService
{
    /// <summary>
    /// Gets clients who rented cars of the specified model, ordered by name.
    /// </summary>
    /// <param name="modelName">Model name to filter.</param>
    public async Task<IEnumerable<ClientDto>> GetClientsByModelAsync(string modelName)
    {
        var rentals = await rentalRepository.GetAllAsync();
        var clients = await clientRepository.GetAllAsync();
        var cars = await carRepository.GetAllAsync();
        var generations = await generationRepository.GetAllAsync();
        var models = await modelRepository.GetAllAsync();

        var modelIds = models
            .Where(m => string.Equals(m.Name, modelName, StringComparison.OrdinalIgnoreCase))
            .Select(m => m.Id)
            .ToHashSet();

        var generationIds = generations.Where(g => modelIds.Contains(g.ModelId)).Select(g => g.Id).ToHashSet();
        var carIds = cars.Where(c => generationIds.Contains(c.GenerationId)).Select(c => c.Id).ToHashSet();

        var clientIds = rentals
            .Where(r => carIds.Contains(r.CarId))
            .Select(r => r.ClientId)
            .Distinct()
            .ToList();

        var result = clients
            .Where(c => clientIds.Contains(c.Id))
            .OrderBy(c => c.FullName);

        return mapper.Map<IEnumerable<ClientDto>>(result);
    }

    /// <summary>
    /// Gets cars currently rented at the given reference date.
    /// </summary>
    /// <param name="referenceDate">Point-in-time to check active rentals.</param>
    public async Task<IEnumerable<CarDto>> GetCurrentlyRentedCarsAsync(DateTime referenceDate)
    {
        var rentals = await rentalRepository.GetAllAsync();
        var cars = await carRepository.GetAllAsync();

        var activeCarIds = rentals
            .Where(r => referenceDate >= r.StartTime && referenceDate < r.StartTime.AddHours(r.DurationHours))
            .Select(r => r.CarId)
            .Distinct()
            .ToHashSet();

        var activeCars = cars.Where(c => activeCarIds.Contains(c.Id));
        return mapper.Map<IEnumerable<CarDto>>(activeCars);
    }

    /// <summary>
    /// Returns top N cars by rental count.
    /// </summary>
    /// <param name="count">Number of cars to return.</param>
    public async Task<IEnumerable<CarRentalCountDto>> GetTopMostRentedCarsAsync(int count)
    {
        if (count <= 0)
        {
            return [];
        }

        var rentals = await rentalRepository.GetAllAsync();
        var cars = await carRepository.GetAllAsync();

        var topCarIds = rentals
            .GroupBy(r => r.CarId)
            .Select(g => new { CarId = g.Key, RentalCount = g.Count() })
            .OrderByDescending(g => g.RentalCount)
            .Take(count)
            .ToList();

        var result = topCarIds
            .Join(
                cars,
                rc => rc.CarId,
                car => car.Id,
                (rc, car) => new CarRentalCountDto
                {
                    Car = mapper.Map<CarDto>(car),
                    RentalCount = rc.RentalCount
                })
            .ToList();

        return result;
    }

    /// <summary>
    /// Returns rental count for every car.
    /// </summary>
    public async Task<IEnumerable<CarRentalCountDto>> GetRentalCountPerCarAsync()
    {
        var rentals = await rentalRepository.GetAllAsync();
        var cars = await carRepository.GetAllAsync();

        var counts = rentals
            .GroupBy(r => r.CarId)
            .Select(g => new { CarId = g.Key, RentalCount = g.Count() })
            .ToDictionary(g => g.CarId, g => g.RentalCount);

        var result = cars
            .Select(car => new CarRentalCountDto
            {
                Car = mapper.Map<CarDto>(car),
                RentalCount = counts.TryGetValue(car.Id, out var count) ? count : 0
            })
            .ToList();

        return result;
    }

    /// <summary>
    /// Returns top N clients by total spending.
    /// </summary>
    /// <param name="count">Number of clients to return.</param>
    public async Task<IEnumerable<ClientTotalSpentDto>> GetTopClientsBySpendingAsync(int count)
    {
        if (count <= 0)
        {
            return [];
        }

        var rentals = await rentalRepository.GetAllAsync();
        var clients = await clientRepository.GetAllAsync();

        var totals = rentals
            .GroupBy(r => r.ClientId)
            .Select(g => new { ClientId = g.Key, TotalSpent = g.Sum(r => r.TotalCost) })
            .OrderByDescending(x => x.TotalSpent)
            .Take(count)
            .ToList();

        var result = totals
            .Join(
                clients,
                t => t.ClientId,
                c => c.Id,
                (t, c) => new ClientTotalSpentDto
                {
                    Client = mapper.Map<ClientDto>(c),
                    TotalSpent = t.TotalSpent
                })
            .ToList();

        return result;
    }
}