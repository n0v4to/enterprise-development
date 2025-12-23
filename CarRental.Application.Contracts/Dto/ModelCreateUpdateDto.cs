using CarRental.Domain.Models;

namespace CarRental.Application.Contracts.Dto;

/// <summary>
/// Data transfer object for creating or updating a Model entity.
/// </summary>
public class ModelCreateUpdateDto
{
    /// <summary>
    /// Name of the model.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Type of drive (FWD, RWD, AWD).
    /// </summary>
    public required Domain.Models.DriveType DriveType { get; set; }

    /// <summary>
    /// Number of seats in the car.
    /// </summary>
    public required int Seats { get; set; }

    /// <summary>
    /// Type of the car body (Sedan, SUV, Hatchback, etc.).
    /// </summary>
    public required BodyType BodyType { get; set; }

    /// <summary>
    /// Class of the car (B, C, D, E).
    /// </summary>
    public required CarClass Class { get; set; }
}