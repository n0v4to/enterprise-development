using CarRental.Domain;
using CarRental.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Infrastructure.EfCore.Repositories;

/// <summary>
/// Represents a repository for managing cars.
/// </summary>
/// <param name="context">The database context.</param>
public class CarRepository(CarRentalDbContext context) : IRepository<Car>
{
    /// <summary>
    /// Adds a new car asynchronously.
    /// </summary>
    /// <param name="entity">The car to add.</param>
    public async Task AddAsync(Car entity)
    {
        await context.Cars.AddAsync(entity);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Deletes a car by its identifier asynchronously.
    /// </summary>
    /// <param name="id">The identifier of the car to delete.</param>
    public async Task DeleteAsync(int id)
    {
        var car = await context.Cars.FindAsync(id);
        if (car != null)
        {
            context.Cars.Remove(car);
            await context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Gets all cars asynchronously.
    /// </summary>
    /// <returns>A collection of all cars.</returns>
    public async Task<IEnumerable<Car>> GetAllAsync()
    {
        return await context.Cars.ToListAsync();
    }

    /// <summary>
    /// Gets a car by its identifier asynchronously.
    /// </summary>
    /// <param name="id">The identifier of the car.</param>
    /// <returns>The car with the specified identifier.</returns>
    public async Task<Car?> GetByIdAsync(int id)
    {
        return await context.Cars.FindAsync(id);
    }

    /// <summary>
    /// Updates an existing car asynchronously.
    /// </summary>
    /// <param name="entity">The car to update.</param>
    public async Task UpdateAsync(Car entity)
    {
        context.Cars.Update(entity);
        await context.SaveChangesAsync();
    }
}