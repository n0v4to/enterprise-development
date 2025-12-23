using CarRental.Application.Contracts;
using CarRental.Application.Contracts.Dto;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Host.Controllers;

/// <summary>
/// CRUD API controller for model generations.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ModelGenerationController(IApplicationService<ModelGenerationDto, ModelGenerationCreateUpdateDto> appService, ILogger<ModelGenerationController> logger)
    : CrudControllerBase<ModelGenerationDto, ModelGenerationCreateUpdateDto>(appService, logger);