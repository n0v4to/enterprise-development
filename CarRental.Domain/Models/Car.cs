namespace CarRental.Domain.Models;

/// <summary>
/// Represents a car in the rental system.
/// </summary>
public class Car
{
    public int Id { get; set; }

    /// <summary>License plate of the car.</summary>
    public required string LicensePlate { get; set; }

    /// <summary>Color of the car.</summary>
    public required string Color { get; set; }

    /// <summary>Generation of the car model.</summary>
    public required ModelGeneration Generation { get; set; }
}