using CarRental.Application.Contracts;
using CarRental.Application.Contracts.Dto;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Host.Controllers;

/// <summary>
/// CRUD API controller for models.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ModelController(IApplicationService<ModelDto, ModelCreateUpdateDto> appService, ILogger<ModelController> logger)
    : CrudControllerBase<ModelDto, ModelCreateUpdateDto>(appService, logger);