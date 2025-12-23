using CarRental.Application.Contracts;
using CarRental.Application.Contracts.Dto;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Host.Controllers;

/// <summary>
/// CRUD API controller for cars.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class CarController(IApplicationService<CarDto, CarCreateUpdateDto> appService, ILogger<CarController> logger)
    : CrudControllerBase<CarDto, CarCreateUpdateDto>(appService, logger);