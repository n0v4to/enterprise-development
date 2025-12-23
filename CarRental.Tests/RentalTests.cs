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
public class RentalTests(DataSeed data) : IClassFixture<DataSeed>
{
    /// <summary>
    /// Returns all clients who rented the Toyota Corolla, ordered by full name.
    /// </summary>
    [Fact]
    public void ClientsByModelShouldReturnClientsOrderedByFullName()
    {
        var toyotaCorollaModelId = data.Models.First(m => m.Name == "Toyota Corolla").Id;
        var toyotaCorollaGenerationIds = data.Generations
            .Where(g => g.ModelId == toyotaCorollaModelId)
            .Select(g => g.Id)
            .ToList();
        var toyotaCorollaCarIds = data.Cars
            .Where(c => toyotaCorollaGenerationIds.Contains(c.GenerationId))
            .Select(c => c.Id)
            .ToList();

        var clients = data.Rentals
            .Where(r => toyotaCorollaCarIds.Contains(r.CarId))
            .Select(r => data.Clients.First(c => c.Id == r.ClientId))
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

        var currentlyRentedCarIds = data.Rentals
            .Where(r => r.StartTime <= referenceDate && referenceDate <= r.StartTime.AddHours(r.DurationHours))
            .Select(r => r.CarId)
            .Distinct()
            .ToList();

        var currentlyRented = data.Cars
            .Where(c => currentlyRentedCarIds.Contains(c.Id))
            .ToList();

        var expectedCarIds = new List<int> { 1, 3, 4, 2 };

        Assert.Equal(expectedCarIds.Count, currentlyRented.Count);
        Assert.All(expectedCarIds, id => Assert.Contains(currentlyRented, c => c.Id == id));
    }

    /// <summary>
    /// Returns top 5 most frequently rented cars.
    /// </summary>
    [Fact]
    public void Top5MostRentedCarsShouldReturnCorrectCars()
    {
        var topCarIds = data.Rentals
            .GroupBy(r => r.CarId)
            .OrderByDescending(g => g.Count())
            .Take(5)
            .Select(g => g.Key)
            .ToList();

        var topCars = data.Cars
            .Where(c => topCarIds.Contains(c.Id))
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
        var counts = data.Cars.ToDictionary(
            car => car.LicensePlate,
            car => data.Rentals.Count(r => r.CarId == car.Id)
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
        var top5 = data.Clients
            .Select(c => new
            {
                c.FullName,
                TotalSpent = data.Rentals.Where(r => r.ClientId == c.Id).Sum(r => r.TotalCost)
            })
            .OrderByDescending(x => x.TotalSpent)
            .Take(5)
            .ToList();

        var expected = new List<string> { "Sergey Orlov", "Anna Fedorova", "Pavel Smirnov", "Ivan Petrov", "Elena Ivanova" };

        foreach (var name in expected)
            Assert.Contains(top5, x => x.FullName == name);
    }
}
