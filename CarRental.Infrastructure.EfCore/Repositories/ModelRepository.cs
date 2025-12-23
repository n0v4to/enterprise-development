using CarRental.Domain;
using CarRental.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Infrastructure.EfCore.Repositories;

/// <summary>
/// Represents a repository for managing models.
/// </summary>
/// <param name="context">The database context.</param>
public class ModelRepository(CarRentalDbContext context) : IRepository<Model>
{
    /// <summary>
    /// Adds a new model asynchronously.
    /// </summary>
    /// <param name="entity">The model to add.</param>
    public async Task AddAsync(Model entity)
    {
        await context.Models.AddAsync(entity);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Deletes a model by its identifier asynchronously.
    /// </summary>
    /// <param name="id">The identifier of the model to delete.</param>
    public async Task DeleteAsync(int id)
    {
        var model = await context.Models.FindAsync(id);
        if (model != null)
        {
            context.Models.Remove(model);
            await context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Gets all models asynchronously.
    /// </summary>
    /// <returns>A collection of all models.</returns>
    public async Task<IEnumerable<Model>> GetAllAsync()
    {
        return await context.Models.ToListAsync();
    }

    /// <summary>
    /// Gets a model by its identifier asynchronously.
    /// </summary>
    /// <param name="id">The identifier of the model.</param>
    /// <returns>The model with the specified identifier.</returns>
    public async Task<Model?> GetByIdAsync(int id)
    {
        return await context.Models.FindAsync(id);
    }

    /// <summary>
    /// Updates an existing model asynchronously.
    /// </summary>
    /// <param name="entity">The model to update.</param>
    public async Task UpdateAsync(Model entity)
    {
        context.Models.Update(entity);
        await context.SaveChangesAsync();
    }
}