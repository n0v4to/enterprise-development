namespace CarRental.Domain.Models;

/// <summary>
/// Represents a car model in the rental system.
/// </summary>
public class Model
{
    /// <summary>Name of the model.</summary>
    public string Name { get; set; }

    /// <summary>Type of drive (FWD, RWD, AWD).</summary>
    public string DriveType { get; set; }

    /// <summary>Number of seats in the car.</summary>
    public int Seats { get; set; }

    /// <summary>Type of the car body (Sedan, SUV, Hatchback, etc.).</summary>
    public string BodyType { get; set; }

    /// <summary>Class of the car (B, C, D, E).</summary>
    public string Class { get; set; }
}
