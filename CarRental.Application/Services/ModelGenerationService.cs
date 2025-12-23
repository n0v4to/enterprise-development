using AutoMapper;
using CarRental.Application.Contracts;
using CarRental.Application.Contracts.Dto;
using CarRental.Domain;
using CarRental.Domain.Models;
using System.Linq;

namespace CarRental.Application.Services;

/// <summary>
/// Application service for model generation CRUD operations.
/// </summary>
public class ModelGenerationService(IRepository<ModelGeneration> generationRepository, IRepository<Model> modelRepository, IMapper mapper)
    : IApplicationService<ModelGenerationDto, ModelGenerationCreateUpdateDto>
{
    /// <summary>
    /// Returns all model generations.
    /// </summary>
    public async Task<IEnumerable<ModelGenerationDto>> GetAllAsync()
    {
        var generations = await generationRepository.GetAllAsync();
        return mapper.Map<IEnumerable<ModelGenerationDto>>(generations);
    }

    /// <summary>
    /// Returns model generation by identifier or null.
    /// </summary>
    /// <param name="id">Generation identifier.</param>
    public async Task<ModelGenerationDto?> GetByIdAsync(int id)
    {
        var generation = await generationRepository.GetByIdAsync(id);
        return mapper.Map<ModelGenerationDto?>(generation);
    }

    /// <summary>
    /// Creates a new model generation after validating model existence.
    /// </summary>
    /// <param name="createDto">Generation for creation.</param>
    public async Task<ModelGenerationDto> CreateAsync(ModelGenerationCreateUpdateDto createDto)
    {
        _ = await modelRepository.GetByIdAsync(createDto.ModelId) ?? throw new KeyNotFoundException("Model with the specified ID does not exist.");
        var nextId = (await generationRepository.GetAllAsync()).Select(g => g.Id).DefaultIfEmpty(0).Max() + 1;

        var generation = mapper.Map<ModelGeneration>(createDto);
        generation.Id = nextId;
        await generationRepository.AddAsync(generation);
        return mapper.Map<ModelGenerationDto>(generation);
    }

    /// <summary>
    /// Updates existing generation.
    /// </summary>
    /// <param name="id">Generation identifier.</param>
    /// <param name="updateDto">Updated generation.</param>
    public async Task<ModelGenerationDto> UpdateAsync(int id, ModelGenerationCreateUpdateDto updateDto)
    {
        var generation = await generationRepository.GetByIdAsync(id) ?? throw new KeyNotFoundException("Model generation with the specified ID does not exist.");

        _ = await modelRepository.GetByIdAsync(updateDto.ModelId) ?? throw new KeyNotFoundException("Model with the specified ID does not exist.");
        mapper.Map(updateDto, generation);
        await generationRepository.UpdateAsync(generation);
        return mapper.Map<ModelGenerationDto>(generation);
    }

    /// <summary>
    /// Deletes model generation by identifier; returns false if not found.
    /// </summary>
    /// <param name="id">Generation identifier.</param>
    public async Task<bool> DeleteAsync(int id)
    {
        var generation = await generationRepository.GetByIdAsync(id);
        if (generation is null)
        {
            return false;
        }

        await generationRepository.DeleteAsync(id);
        return true;
    }
}