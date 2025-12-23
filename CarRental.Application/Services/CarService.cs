using AutoMapper;
using CarRental.Application.Contracts;
using CarRental.Application.Contracts.Dto;
using CarRental.Domain;
using CarRental.Domain.Models;
using System.Linq;

namespace CarRental.Application.Services;

/// <summary>
/// Application service for car CRUD operations.
/// </summary>
public class CarService(IRepository<Car> carRepository, IRepository<ModelGeneration> generationRepository, IMapper mapper)
    : IApplicationService<CarDto, CarCreateUpdateDto>
{
    /// <summary>
    /// Returns all cars.
    /// </summary>
    public async Task<IEnumerable<CarDto>> GetAllAsync()
    {
        var cars = await carRepository.GetAllAsync();
        return mapper.Map<IEnumerable<CarDto>>(cars);
    }

    /// <summary>
    /// Returns car by identifier or null if not found.
    /// </summary>
    /// <param name="id">Car identifier.</param>
    public async Task<CarDto?> GetByIdAsync(int id)
    {
        var car = await carRepository.GetByIdAsync(id);
        return mapper.Map<CarDto?>(car);
    }

    /// <summary>
    /// Creates a new car after validating generation existence.
    /// </summary>
    /// <param name="createDto">Car for creation.</param>
    public async Task<CarDto> CreateAsync(CarCreateUpdateDto createDto)
    {
        _ = await generationRepository.GetByIdAsync(createDto.GenerationId) ?? throw new KeyNotFoundException("Model generation with the specified ID does not exist.");
        var nextId = (await carRepository.GetAllAsync()).Select(c => c.Id).DefaultIfEmpty(0).Max() + 1;

        var car = mapper.Map<Car>(createDto);
        car.Id = nextId;
        await carRepository.AddAsync(car);
        return mapper.Map<CarDto>(car);
    }

    /// <summary>
    /// Updates existing car.
    /// </summary>
    /// <param name="id">Car identifier.</param>
    /// <param name="updateDto">Updated car.</param>
    public async Task<CarDto> UpdateAsync(int id, CarCreateUpdateDto updateDto)
    {
        var car = await carRepository.GetByIdAsync(id) ?? throw new KeyNotFoundException("Car with the specified ID does not exist.");

        _ = await generationRepository.GetByIdAsync(updateDto.GenerationId) ?? throw new KeyNotFoundException("Model generation with the specified ID does not exist.");
        mapper.Map(updateDto, car);
        await carRepository.UpdateAsync(car);
        return mapper.Map<CarDto>(car);
    }

    /// <summary>
    /// Deletes car by identifier; returns false if not found.
    /// </summary>
    /// <param name="id">Car identifier.</param>
    public async Task<bool> DeleteAsync(int id)
    {
        var car = await carRepository.GetByIdAsync(id);
        if (car is null)
        {
            return false;
        }

        await carRepository.DeleteAsync(id);
        return true;
    }
}