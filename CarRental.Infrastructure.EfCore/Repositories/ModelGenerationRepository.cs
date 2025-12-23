using CarRental.Domain;
using CarRental.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Infrastructure.EfCore.Repositories;

/// <summary>
/// Represents a repository for managing model generations.
/// </summary>
/// <param name="context">The database context.</param>
public class ModelGenerationRepository(CarRentalDbContext context) : IRepository<ModelGeneration>
{
    /// <summary>
    /// Adds a new model generation asynchronously.
    /// </summary>
    /// <param name="entity">The model generation to add.</param>
    public async Task AddAsync(ModelGeneration entity)
    {
        await context.ModelGenerations.AddAsync(entity);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Deletes a model generation by its identifier asynchronously.
    /// </summary>
    /// <param name="id">The identifier of the model generation to delete.</param>
    public async Task DeleteAsync(int id)
    {
        var modelGeneration = await context.ModelGenerations.FindAsync(id);
        if (modelGeneration != null)
        {
            context.ModelGenerations.Remove(modelGeneration);
            await context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Gets all model generations asynchronously.
    /// </summary>
    /// <returns>A collection of all model generations.</returns>
    public async Task<IEnumerable<ModelGeneration>> GetAllAsync()
    {
        return await context.ModelGenerations.ToListAsync();
    }

    /// <summary>
    /// Gets a model generation by its identifier asynchronously.
    /// </summary>
    /// <param name="id">The identifier of the model generation.</param>
    /// <returns>The model generation with the specified identifier.</returns>
    public async Task<ModelGeneration?> GetByIdAsync(int id)
    {
        return await context.ModelGenerations.FindAsync(id);
    }

    /// <summary>
    /// Updates an existing model generation asynchronously.
    /// </summary>
    /// <param name="entity">The model generation to update.</param>
    public async Task UpdateAsync(ModelGeneration entity)
    {
        context.ModelGenerations.Update(entity);
        await context.SaveChangesAsync();
    }
}