using CarRental.Application.Contracts;
using CarRental.Application.Contracts.Dto;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Host.Controllers;

/// <summary>
/// CRUD API controller for rentals.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class RentalController(IApplicationService<RentalDto, RentalCreateUpdateDto> appService, ILogger<RentalController> logger)
    : CrudControllerBase<RentalDto, RentalCreateUpdateDto>(appService, logger);