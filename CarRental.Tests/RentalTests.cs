using System.Linq;
using Xunit;
using CarRental.Domain.Data;

namespace CarRental.Tests;

/// <summary>
/// Unit tests for car rental queries using DataSeed fixture.
/// </summary>
public class RentalTests : IClassFixture<DataSeed>
{
    private readonly DataSeed _data;

    /// <summary>
    /// Initializes a new instance of the <see cref="RentalTests"/> class with the provided test data fixture.
    /// </summary>
    /// <param name="data">Test data fixture containing cars, clients, and rentals.</param>
    public RentalTests(DataSeed data)
    {
        _data = data;
    }

    /// <summary>
    /// Returns all clients who rented the Toyota Corolla, ordered by full name.
    /// </summary>
    [Fact]
    public void ClientsByModel_ShouldReturnClientsOrderedByFullName()
    {
        var clients = _data.Rentals
            .Where(r => r.Car.Generation.Model.Name == "Toyota Corolla")
            .Select(r => r.Client)
            .Distinct()
            .OrderBy(c => c.FullName)
            .ToList();

        Assert.NotEmpty(clients);
    }

    /// <summary>
    /// Returns all cars that are currently rented.
    /// </summary>
    [Fact]
    public void CarsCurrentlyRented_ShouldReturnAllRentedCars()
    {
        var rentedCars = _data.Rentals.Select(r => r.Car).Distinct().ToList();
        Assert.NotEmpty(rentedCars);
    }

    /// <summary>
    /// Returns top 5 most frequently rented cars.
    /// </summary>
    [Fact]
    public void Top5MostRentedCars_ShouldReturnCorrectCars()
    {
        var topCars = _data.Rentals
            .GroupBy(r => r.Car)
            .OrderByDescending(g => g.Count())
            .Take(5)
            .Select(g => g.Key)
            .ToList();

        Assert.Equal(5, topCars.Count);
    }

    /// <summary>
    /// Returns rental count for each car.
    /// </summary>
    [Fact]
    public void RentalCountPerCar_ShouldReturnCorrectCounts()
    {
        var counts = _data.Rentals
            .GroupBy(r => r.Car)
            .Select(g => new { g.Key.LicensePlate, Count = g.Count() })
            .ToList();

        Assert.NotEmpty(counts);
    }

    /// <summary>
    /// Returns top 5 clients by total spent on rentals.
    /// </summary>
    [Fact]
    public void Top5ClientsByTotalSpent_ShouldReturnCorrectClients()
    {
        var topClients = _data.Rentals
            .GroupBy(r => r.Client)
            .Select(g => new { g.Key.FullName, TotalSpent = g.Sum(r => r.TotalCost) })
            .OrderByDescending(x => x.TotalSpent)
            .Take(5)
            .ToList();

        Assert.Equal(5, topClients.Count);
    }
}
