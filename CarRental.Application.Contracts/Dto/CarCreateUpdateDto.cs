namespace CarRental.Application.Contracts.Dto;

/// <summary>
/// Data transfer object for creating or updating a Car entity.
/// </summary>
public class CarCreateUpdateDto
{
    /// <summary>
    /// License plate of the car.
    /// </summary>
    public required string LicensePlate { get; set; }

    /// <summary>
    /// Color of the car.
    /// </summary>
    public required string Color { get; set; }

    /// <summary>
    /// Id of the car model generation.
    /// </summary>
    public required int GenerationId { get; set; }
}