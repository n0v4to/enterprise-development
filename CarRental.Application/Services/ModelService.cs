using AutoMapper;
using CarRental.Application.Contracts;
using CarRental.Application.Contracts.Dto;
using CarRental.Domain;
using CarRental.Domain.Models;
using System.Linq;

namespace CarRental.Application.Services;

/// <summary>
/// Application service for model CRUD operations.
/// </summary>
public class ModelService(IRepository<Model> modelRepository, IMapper mapper)
    : IApplicationService<ModelDto, ModelCreateUpdateDto>
{
    /// <summary>
    /// Returns all models.
    /// </summary>
    public async Task<IEnumerable<ModelDto>> GetAllAsync()
    {
        var models = await modelRepository.GetAllAsync();
        return mapper.Map<IEnumerable<ModelDto>>(models);
    }

    /// <summary>
    /// Returns model by identifier or null.
    /// </summary>
    /// <param name="id">Model identifier.</param>
    public async Task<ModelDto?> GetByIdAsync(int id)
    {
        var model = await modelRepository.GetByIdAsync(id);
        return mapper.Map<ModelDto?>(model);
    }

    /// <summary>
    /// Creates a new model.
    /// </summary>
    /// <param name="createDto">Model for creation.</param>
    public async Task<ModelDto> CreateAsync(ModelCreateUpdateDto createDto)
    {
        var nextId = (await modelRepository.GetAllAsync()).Select(m => m.Id).DefaultIfEmpty(0).Max() + 1;

        var model = mapper.Map<Model>(createDto);
        model.Id = nextId;
        await modelRepository.AddAsync(model);
        return mapper.Map<ModelDto>(model);
    }

    /// <summary>
    /// Updates existing model.
    /// </summary>
    /// <param name="id">Model identifier.</param>
    /// <param name="updateDto">Updated model.</param>
    public async Task<ModelDto> UpdateAsync(int id, ModelCreateUpdateDto updateDto)
    {
        var model = await modelRepository.GetByIdAsync(id) ?? throw new KeyNotFoundException("Model with the specified ID does not exist.");

        mapper.Map(updateDto, model);
        await modelRepository.UpdateAsync(model);
        return mapper.Map<ModelDto>(model);
    }

    /// <summary>
    /// Deletes model by identifier; returns false if not found.
    /// </summary>
    /// <param name="id">Model identifier.</param>
    public async Task<bool> DeleteAsync(int id)
    {
        var model = await modelRepository.GetByIdAsync(id);
        if (model is null)
        {
            return false;
        }

        await modelRepository.DeleteAsync(id);
        return true;
    }
}