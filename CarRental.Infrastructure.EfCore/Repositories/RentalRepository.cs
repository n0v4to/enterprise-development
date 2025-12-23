using CarRental.Domain;
using CarRental.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Infrastructure.EfCore.Repositories;

/// <summary>
/// Represents a repository for managing rentals.
/// </summary>
/// <param name="context">The database context.</param>
public class RentalRepository(CarRentalDbContext context) : IRepository<Rental>
{
    /// <summary>
    /// Adds a new rental asynchronously.
    /// </summary>
    /// <param name="entity">The rental to add.</param>
    public async Task AddAsync(Rental entity)
    {
        await context.Rentals.AddAsync(entity);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Deletes a rental by its identifier asynchronously.
    /// </summary>
    /// <param name="id">The identifier of the rental to delete.</param>
    public async Task DeleteAsync(int id)
    {
        var rental = await context.Rentals.FindAsync(id);
        if (rental != null)
        {
            context.Rentals.Remove(rental);
            await context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Gets all rentals asynchronously.
    /// </summary>
    /// <returns>A collection of all rentals.</returns>
    public async Task<IEnumerable<Rental>> GetAllAsync()
    {
        return await context.Rentals.ToListAsync();
    }

    /// <summary>
    /// Gets a rental by its identifier asynchronously.
    /// </summary>
    /// <param name="id">The identifier of the rental.</param>
    /// <returns>The rental with the specified identifier.</returns>
    public async Task<Rental?> GetByIdAsync(int id)
    {
        return await context.Rentals.FindAsync(id);
    }

    /// <summary>
    /// Updates an existing rental asynchronously.
    /// </summary>
    /// <param name="entity">The rental to update.</param>
    public async Task UpdateAsync(Rental entity)
    {
        context.Rentals.Update(entity);
        await context.SaveChangesAsync();
    }
}