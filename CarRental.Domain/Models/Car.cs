namespace CarRental.Domain.Models;

/// <summary>
/// Represents a car in the rental system.
/// </summary>
public class Car
{
    /// <summary>License plate of the car.</summary>
    public string LicensePlate { get; set; }

    /// <summary>Color of the car.</summary>
    public string Color { get; set; }

    /// <summary>Generation of the car model.</summary>
    public ModelGeneration Generation { get; set; }
}
