using CarRental.Domain.Data;
using CarRental.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CarRental.Tests;

/// <summary>
/// Unit tests for car rental queries using DataSeed fixture.
/// </summary>
public class RentalTests : IClassFixture<DataSeed>
{
    private readonly DataSeed _data;

    /// <summary>
    /// Primary constructor using the DataSeed fixture.
    /// </summary>
    public RentalTests(DataSeed data) => _data = data;

    /// <summary>
    /// Returns all clients who rented the Toyota Corolla, ordered by full name.
    /// </summary>
    [Fact]
    public void ClientsByModelShouldReturnClientsOrderedByFullName()
    {
        var clients = _data.Rentals
            .Where(r => r.Car?.Generation?.Model?.Name == "Toyota Corolla")
            .Select(r => r.Client)
            .Where(c => c != null)
            .Distinct()
            .OrderBy(c => c.FullName)
            .ToList();

        Assert.NotEmpty(clients);
    }

    /// <summary>
    /// Returns all cars that are currently rented at a given reference date.
    /// </summary>
    [Fact]
    public void CarsCurrentlyRentedShouldReturnCorrectCars()
{
    var referenceDate = new DateTime(2025, 11, 28, 12, 0, 0);

    var currentlyRented = _data.Rentals
        .Where(r => r.StartTime <= referenceDate && referenceDate <= r.StartTime.AddHours(r.DurationHours))
        .Select(r => r.Car)
        .Distinct()
        .ToList();

    var expectedCars = new List<Car> { _data.Cars[0], _data.Cars[2], _data.Cars[3], _data.Cars[1] };

    Assert.Equal(expectedCars.Count, currentlyRented.Count);
    Assert.All(expectedCars, car => Assert.Contains(car, currentlyRented));
}

    /// <summary>
    /// Returns top 5 most frequently rented cars.
    /// </summary>
    [Fact]
    public void Top5MostRentedCarsShouldReturnCorrectCars()
    {
        var topCars = _data.Rentals
            .Where(r => r.Car != null)
            .GroupBy(r => r.Car)
            .OrderByDescending(g => g.Count())
            .Take(5)
            .Select(g => g.Key)
            .ToList();

        var expectedTop = new[] { "A111AA", "B222BB", "C333CC", "F666FF", "G777GG" };
        Assert.Equal(expectedTop.Length, topCars.Count);
        foreach (var license in expectedTop)
        {
            Assert.Contains(topCars, c => c.LicensePlate == license);
        }
    }

    /// <summary>
    /// Returns rental count for each car.
    /// </summary>
    [Fact]
    public void RentalCountPerCarShouldReturnCorrectCounts()
    {
        var counts = _data.Cars.ToDictionary(
            car => car.LicensePlate,
            car => _data.Rentals.Count(r => r.Car == car)
        );

        Assert.Equal(3, counts["A111AA"]);
        Assert.Equal(2, counts["B222BB"]);
        Assert.Equal(2, counts["C333CC"]);
        Assert.Equal(1, counts["D444DD"]);
        Assert.Equal(1, counts["E555EE"]);
    }

    /// <summary>
    /// Returns top 5 clients by total spent on rentals.
    /// </summary>
    [Fact]
    public void Top5ClientsByTotalSpentShouldReturnCorrectClients()
    {
        var top5 = _data.Clients
            .Select(c => new { c.FullName, TotalSpent = _data.Rentals.Where(r => r.Client == c).Sum(r => r.DurationHours * r.Car.Generation.HourlyRate) })
            .OrderByDescending(x => x.TotalSpent)
            .Take(5)
            .ToList();

        var expected = new List<string> { "Sergey Orlov", "Anna Fedorova", "Pavel Smirnov", "Ivan Petrov", "Elena Ivanova" };

        foreach (var name in expected)
            Assert.Contains(top5, x => x.FullName == name);
    }
}
