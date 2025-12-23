using AutoMapper;
using CarRental.Application.Contracts;
using CarRental.Application.Contracts.Dto;
using CarRental.Domain;
using CarRental.Domain.Models;
using System.Linq;

namespace CarRental.Application.Services;

/// <summary>
/// Application service for client CRUD operations.
/// </summary>
public class ClientService(IRepository<Client> clientRepository, IMapper mapper)
    : IApplicationService<ClientDto, ClientCreateUpdateDto>
{
    /// <summary>
    /// Returns all clients.
    /// </summary>
    public async Task<IEnumerable<ClientDto>> GetAllAsync()
    {
        var clients = await clientRepository.GetAllAsync();
        return mapper.Map<IEnumerable<ClientDto>>(clients);
    }

    /// <summary>
    /// Returns client by identifier or null.
    /// </summary>
    /// <param name="id">Client identifier.</param>
    public async Task<ClientDto?> GetByIdAsync(int id)
    {
        var client = await clientRepository.GetByIdAsync(id);
        return mapper.Map<ClientDto?>(client);
    }

    /// <summary>
    /// Creates a new client.
    /// </summary>
    /// <param name="createDto">Client for creation.</param>
    public async Task<ClientDto> CreateAsync(ClientCreateUpdateDto createDto)
    {
        var nextId = (await clientRepository.GetAllAsync()).Select(c => c.Id).DefaultIfEmpty(0).Max() + 1;

        var client = mapper.Map<Client>(createDto);
        client.Id = nextId;
        await clientRepository.AddAsync(client);
        return mapper.Map<ClientDto>(client);
    }

    /// <summary>
    /// Updates existing client.
    /// </summary>
    /// <param name="id">Client identifier.</param>
    /// <param name="updateDto">Updated client.</param>
    public async Task<ClientDto> UpdateAsync(int id, ClientCreateUpdateDto updateDto)
    {
        var client = await clientRepository.GetByIdAsync(id) ?? throw new KeyNotFoundException("Client with the specified ID does not exist.");

        mapper.Map(updateDto, client);
        await clientRepository.UpdateAsync(client);
        return mapper.Map<ClientDto>(client);
    }

    /// <summary>
    /// Deletes client by identifier; returns false if not found.
    /// </summary>
    /// <param name="id">Client identifier.</param>
    public async Task<bool> DeleteAsync(int id)
    {
        var client = await clientRepository.GetByIdAsync(id);
        if (client is null)
        {
            return false;
        }

        await clientRepository.DeleteAsync(id);
        return true;
    }
}