using AutoMapper;
using CarRental.Application.Contracts;
using CarRental.Application.Contracts.Dto;
using CarRental.Domain;
using CarRental.Domain.Models;

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
        var clients = await clientRepository.GetAllAsync();
        var cars = await carRepository.GetAllAsync();

        var clientNames = clients.ToDictionary(c => c.Id, c => c.FullName);
        var carPlates = cars.ToDictionary(c => c.Id, c => c.LicensePlate);

        return rentals.Select(rental => new RentalDto
        {
            Id = rental.Id,
            ClientFullName = clientNames.GetValueOrDefault(rental.ClientId, "Unknown"),
            CarLicensePlate = carPlates.GetValueOrDefault(rental.CarId, "Unknown"),
            StartTime = rental.StartTime,
            DurationHours = rental.DurationHours,
            TotalCost = rental.TotalCost
        });
    }

    /// <summary>
    /// Returns rental by identifier or null.
    /// </summary>
    /// <param name="id">Rental identifier.</param>
    public async Task<RentalDto?> GetByIdAsync(int id)
    {
        var rental = await rentalRepository.GetByIdAsync(id);
        if (rental is null)
            return null;

        var client = await clientRepository.GetByIdAsync(rental.ClientId);
        var car = await carRepository.GetByIdAsync(rental.CarId);

        return new RentalDto
        {
            Id = rental.Id,
            ClientFullName = client?.FullName ?? "Unknown",
            CarLicensePlate = car?.LicensePlate ?? "Unknown",
            StartTime = rental.StartTime,
            DurationHours = rental.DurationHours,
            TotalCost = rental.TotalCost
        };
    }

    /// <summary>
    /// Creates a new rental after validating related entities.
    /// </summary>
    /// <param name="createDto">Rental for creation.</param>
    public async Task<RentalDto> CreateAsync(RentalCreateUpdateDto createDto)
    {
        var client = await clientRepository.GetByIdAsync(createDto.ClientId) ?? throw new KeyNotFoundException("Client with the specified ID does not exist.");
        var car = await carRepository.GetByIdAsync(createDto.CarId) ?? throw new KeyNotFoundException("Car with the specified ID does not exist.");

        var nextId = (await rentalRepository.GetAllAsync()).Select(r => r.Id).DefaultIfEmpty(0).Max() + 1;

        var rental = mapper.Map<Rental>(createDto);
        rental.Id = nextId;
        await rentalRepository.AddAsync(rental);

        return new RentalDto
        {
            Id = rental.Id,
            ClientFullName = client.FullName,
            CarLicensePlate = car.LicensePlate,
            StartTime = rental.StartTime,
            DurationHours = rental.DurationHours,
            TotalCost = rental.TotalCost
        };
    }

    /// <summary>
    /// Updates rental.
    /// </summary>
    /// <param name="id">Rental identifier.</param>
    /// <param name="updateDto">Updated rental.</param>
    public async Task<RentalDto> UpdateAsync(int id, RentalCreateUpdateDto updateDto)
    {
        var rental = await rentalRepository.GetByIdAsync(id) ?? throw new KeyNotFoundException("Rental with the specified ID does not exist.");

        var client = await clientRepository.GetByIdAsync(updateDto.ClientId) ?? throw new KeyNotFoundException("Client with the specified ID does not exist.");
        var car = await carRepository.GetByIdAsync(updateDto.CarId) ?? throw new KeyNotFoundException("Car with the specified ID does not exist.");

        mapper.Map(updateDto, rental);
        await rentalRepository.UpdateAsync(rental);

        return new RentalDto
        {
            Id = rental.Id,
            ClientFullName = client.FullName,
            CarLicensePlate = car.LicensePlate,
            StartTime = rental.StartTime,
            DurationHours = rental.DurationHours,
            TotalCost = rental.TotalCost
        };
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