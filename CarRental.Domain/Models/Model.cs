namespace CarRental.Domain.Models;

/// <summary>
/// Represents a car model in the rental system.
/// </summary>
public class Model
{
    public int Id { get; set; }

    /// <summary>Name of the model.</summary>
    public required string Name { get; set; }

    /// <summary>Type of drive (FWD, RWD, AWD).</summary>
    public required DriveType DriveType { get; set; }

    /// <summary>Number of seats in the car.</summary>
    public required int Seats { get; set; }

    /// <summary>Type of the car body (Sedan, SUV, Hatchback, etc.).</summary>
    public required BodyType BodyType { get; set; }

    /// <summary>Class of the car (B, C, D, E).</summary>
    public required CarClass Class { get; set; }
}