namespace CarRental.Application.Contracts;

/// <summary>
/// Generic application service interface for CRUD operations.
/// </summary>
/// <typeparam name="TDto">The DTO type for reading entities.</typeparam>
/// <typeparam name="TCreateUpdateDto">The DTO type for creating and updating entities.</typeparam>
public interface IApplicationService<TDto, TCreateUpdateDto>
    where TDto : class
    where TCreateUpdateDto : class
{
    /// <summary>
    /// Gets all entities.
    /// </summary>
    /// <returns>A collection of all entities.</returns>
    public Task<IEnumerable<TDto>> GetAllAsync();

    /// <summary>
    /// Gets an entity by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the entity.</param>
    /// <returns>The entity with the specified identifier, or null if not found.</returns>
    public Task<TDto?> GetByIdAsync(int id);

    /// <summary>
    /// Creates a new entity.
    /// </summary>
    /// <param name="createDto">The DTO containing data for the new entity.</param>
    /// <returns>The created entity.</returns>
    public Task<TDto> CreateAsync(TCreateUpdateDto createDto);

    /// <summary>
    /// Updates an existing entity.
    /// </summary>
    /// <param name="id">The identifier of the entity to update.</param>
    /// <param name="updateDto">The DTO containing updated data.</param>
    /// <returns>The updated entity.</returns>
    public Task<TDto> UpdateAsync(int id, TCreateUpdateDto updateDto);

    /// <summary>
    /// Deletes an entity by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the entity to delete.</param>
    /// <returns>True if the entity was deleted, false if not found.</returns>
    public Task<bool> DeleteAsync(int id);
}