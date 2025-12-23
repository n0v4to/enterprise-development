using CarRental.Domain;
using CarRental.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Infrastructure.EfCore.Repositories;

/// <summary>
/// Represents a repository for managing clients.
/// </summary>
/// <param name="context">The database context.</param>
public class ClientRepository(CarRentalDbContext context) : IRepository<Client>
{
    /// <summary>
    /// Adds a new client asynchronously.
    /// </summary>
    /// <param name="entity">The client to add.</param>
    public async Task AddAsync(Client entity)
    {
        await context.Clients.AddAsync(entity);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Deletes a client by its identifier asynchronously.
    /// </summary>
    /// <param name="id">The identifier of the client to delete.</param>
    public async Task DeleteAsync(int id)
    {
        var client = await context.Clients.FindAsync(id);
        if (client != null)
        {
            context.Clients.Remove(client);
            await context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Gets all clients asynchronously.
    /// </summary>
    /// <returns>A collection of all clients.</returns>
    public async Task<IEnumerable<Client>> GetAllAsync()
    {
        return await context.Clients.ToListAsync();
    }

    /// <summary>
    /// Gets a client by its identifier asynchronously.
    /// </summary>
    /// <param name="id">The identifier of the client.</param>
    /// <returns>The client with the specified identifier.</returns>
    public async Task<Client?> GetByIdAsync(int id)
    {
        return await context.Clients.FindAsync(id);
    }

    /// <summary>
    /// Updates an existing client asynchronously.
    /// </summary>
    /// <param name="entity">The client to update.</param>
    public async Task UpdateAsync(Client entity)
    {
        context.Clients.Update(entity);
        await context.SaveChangesAsync();
    }
}