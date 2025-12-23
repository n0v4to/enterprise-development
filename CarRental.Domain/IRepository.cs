namespace CarRental.Domain;

/// <summary>
/// Represents a generic repository for entities.
/// </summary>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
public interface IRepository<TEntity> where TEntity : class
{
    /// <summary>
    /// Gets all entities.
    /// </summary>
    /// <returns>A collection of all entities.</returns>
    public Task<IEnumerable<TEntity>> GetAllAsync();

    /// <summary>
    /// Gets an entity by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the entity.</param>
    /// <returns>The entity with the specified identifier.</returns>
    public Task<TEntity?> GetByIdAsync(int id);

    /// <summary>
    /// Adds a new entity.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    public Task AddAsync(TEntity entity);

    /// <summary>
    /// Updates an existing entity asynchronously.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    public Task UpdateAsync(TEntity entity);

    /// <summary>
    /// Deletes an entity.
    /// </summary>
    /// <param name="id">The identifier of the entity to delete.</param>
    public Task DeleteAsync(int id);
}