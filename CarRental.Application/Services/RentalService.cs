using AutoMapper;
using CarRental.Application.Contracts;
using CarRental.Application.Contracts.Dto;
using CarRental.Domain;
using CarRental.Domain.Models;
using System.Linq;

namespace CarRental.Application.Services;

/// <summary>
/// Application service for rental CRUD operations.
/// </summary>
public class RentalService(
    IRepository<Rental> rentalRepository,
    IRepository<Client> clientRepository,
    IRepository<Car> carRepository,
    IMapper mapper)
    : IApplicationService<RentalDto, RentalCreateUpdateDto>
{
    /// <summary>
    /// Returns all rentals.
    /// </summary>
    public async Task<IEnumerable<RentalDto>> GetAllAsync()
    {
        var rentals = await rentalRepository.GetAllAsync();
        return mapper.Map<IEnumerable<RentalDto>>(rentals);
    }

    /// <summary>
    /// Returns rental by identifier or null.
    /// </summary>
    /// <param name="id">Rental identifier.</param>
    public async Task<RentalDto?> GetByIdAsync(int id)
    {
        var rental = await rentalRepository.GetByIdAsync(id);
        return mapper.Map<RentalDto?>(rental);
    }

    /// <summary>
    /// Creates a new rental after validating related entities.
    /// </summary>
    /// <param name="createDto">Rental for creation.</param>
    public async Task<RentalDto> CreateAsync(RentalCreateUpdateDto createDto)
    {
        _ = await clientRepository.GetByIdAsync(createDto.ClientId) ?? throw new KeyNotFoundException("Client with the specified ID does not exist.");
        _ = await carRepository.GetByIdAsync(createDto.CarId) ?? throw new KeyNotFoundException("Car with the specified ID does not exist.");

        var nextId = (await rentalRepository.GetAllAsync()).Select(r => r.Id).DefaultIfEmpty(0).Max() + 1;

        var rental = mapper.Map<Rental>(createDto);
        rental.Id = nextId;
        await rentalRepository.AddAsync(rental);
        return mapper.Map<RentalDto>(rental);
    }

    /// <summary>
    /// Updates rental.
    /// </summary>
    /// <param name="id">Rental identifier.</param>
    /// <param name="updateDto">Updated rental.</param>
    public async Task<RentalDto> UpdateAsync(int id, RentalCreateUpdateDto updateDto)
    {
        var rental = await rentalRepository.GetByIdAsync(id) ?? throw new KeyNotFoundException("Rental with the specified ID does not exist.");

        _ = await clientRepository.GetByIdAsync(updateDto.ClientId) ?? throw new KeyNotFoundException("Client with the specified ID does not exist.");
        _ = await carRepository.GetByIdAsync(updateDto.CarId) ?? throw new KeyNotFoundException("Car with the specified ID does not exist.");

        mapper.Map(updateDto, rental);
        await rentalRepository.UpdateAsync(rental);
        return mapper.Map<RentalDto>(rental);
    }

    /// <summary>
    /// Deletes rental by identifier; returns false if not found.
    /// </summary>
    /// <param name="id">Rental identifier.</param>
    public async Task<bool> DeleteAsync(int id)
    {
        var rental = await rentalRepository.GetByIdAsync(id);
        if (rental is null)
        {
            return false;
        }

        await rentalRepository.DeleteAsync(id);
        return true;
    }
}