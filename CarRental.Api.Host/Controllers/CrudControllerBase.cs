using CarRental.Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Host.Controllers;

/// <summary>
/// Base controller for CRUD operations over entities
/// </summary>
/// <typeparam name="TDto">DTO type used for Get requests</typeparam>
/// <typeparam name="TCreateUpdateDto">DTO type used for Post/Put requests</typeparam>
/// <typeparam name="TKey">Type of the DTO identifier</typeparam>
/// <param name="appService">Service used to manipulate DTOs</param>
/// <param name="logger">Logger instance</param>
[Route("api/[controller]")]
[ApiController]
public abstract class CrudControllerBase<TDto, TCreateUpdateDto>(IApplicationService<TDto, TCreateUpdateDto> appService,
    ILogger<CrudControllerBase<TDto, TCreateUpdateDto>> logger) : ControllerBase
    where TDto : class
    where TCreateUpdateDto : class
{
    /// <summary>
    /// Adds a new entity
    /// </summary>
    /// <param name="newDto">New data to create</param>
    /// <returns>The created data</returns>
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<TDto>> Create(TCreateUpdateDto newDto)
    {
        logger.LogInformation("{method} method of {controller} is called with {@dto} parameter", nameof(Create), GetType().Name, newDto);
        try
        {
            var res = await appService.CreateAsync(newDto);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(Create), GetType().Name);
            return CreatedAtAction(nameof(this.Create), res);
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogWarning("Not found exception happened during {method} method of {controller}: {@exception}", nameof(Create), GetType().Name, ex);
            return NotFound($"{ex.Message}");
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(Create), GetType().Name, ex);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Updates an existing entity
    /// </summary>
    /// <param name="id">Identifier of the entity</param>
    /// <param name="newDto">Updated data</param>
    /// <returns>The updated data</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<TDto>> Update([FromRoute] int id, [FromBody] TCreateUpdateDto newDto)
    {
        logger.LogInformation("{method} method of {controller} is called with {key},{@dto} parameters", nameof(Update), GetType().Name, id, newDto);
        try
        {
            var res = await appService.UpdateAsync(id, newDto);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(Update), GetType().Name);
            return Ok(res);
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogWarning("Not found exception happened during {method} method of {controller}: {@exception}", nameof(Update), GetType().Name, ex);
            return NotFound($"{ex.Message}");
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(Update), GetType().Name, ex);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Deletes an entity
    /// </summary>
    /// <param name="id">Identifier of the entity</param>
    [HttpDelete("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Delete(int id)
    {
        logger.LogInformation("{method} method of {controller} is called with {id} parameter", nameof(Delete), GetType().Name, id);
        try
        {
            var res = await appService.DeleteAsync(id);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(Delete), GetType().Name);
            return res ? Ok() : NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(Delete), GetType().Name, ex);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Retrieves all entities
    /// </summary>
    /// <returns>List of all entities</returns>
    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IList<TDto>>> GetAll()
    {
        logger.LogInformation("{method} method of {controller} is called", nameof(GetAll), GetType().Name);
        try
        {
            var res = await appService.GetAllAsync();
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetAll), GetType().Name);
            return Ok(res);
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(GetAll), GetType().Name, ex);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Retrieves a single entity by its identifier
    /// </summary>
    /// <param name="id">Identifier of the entity</param>
    /// <returns>The entity if found; otherwise NotFound</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<TDto>> Get(int id)
    {
        logger.LogInformation("{method} method of {controller} is called with {id} parameter", nameof(Get), GetType().Name, id);
        try
        {
            var res = await appService.GetByIdAsync(id);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(Get), GetType().Name);
            return res is null ? NotFound() : Ok(res);
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(Get), GetType().Name, ex);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }
}