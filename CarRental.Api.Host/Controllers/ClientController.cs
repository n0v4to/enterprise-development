using CarRental.Application.Contracts;
using CarRental.Application.Contracts.Dto;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Host.Controllers;

/// <summary>
/// CRUD API controller for clients.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ClientController(IApplicationService<ClientDto, ClientCreateUpdateDto> appService, ILogger<ClientController> logger)
    : CrudControllerBase<ClientDto, ClientCreateUpdateDto>(appService, logger);