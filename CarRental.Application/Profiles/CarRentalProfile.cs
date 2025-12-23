using AutoMapper;
using CarRental.Application.Contracts.Dto;
using CarRental.Domain.Models;

namespace CarRental.Application.Profiles;

/// <summary>
/// AutoMapper profile for CarRental mappings.
/// </summary>
public class CarRentalProfile : Profile
{
    /// <summary>
    /// Configures entity-to-DTO and DTO-to-entity maps.
    /// </summary>
    public CarRentalProfile()
    {
        CreateMap<Car, CarDto>().ReverseMap();
        CreateMap<Car, CarCreateUpdateDto>().ReverseMap();

        CreateMap<Client, ClientDto>().ReverseMap();
        CreateMap<Client, ClientCreateUpdateDto>().ReverseMap();

        CreateMap<Model, ModelDto>().ReverseMap();
        CreateMap<Model, ModelCreateUpdateDto>().ReverseMap();

        CreateMap<ModelGeneration, ModelGenerationDto>().ReverseMap();
        CreateMap<ModelGeneration, ModelGenerationCreateUpdateDto>().ReverseMap();

        CreateMap<Rental, RentalDto>().ReverseMap();
        CreateMap<Rental, RentalCreateUpdateDto>().ReverseMap();
    }
}