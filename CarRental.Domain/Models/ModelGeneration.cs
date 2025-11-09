namespace CarRental.Domain.Models;

/// <summary>
/// Represents a specific generation of a car model.
/// </summary>
public class ModelGeneration
{
    /// <summary>Model to which this generation belongs.</summary>
    public Model Model { get; set; }

    /// <summary>Year of manufacture.</summary>
    public int Year { get; set; }

    /// <summary>Engine volume in liters.</summary>
    public double EngineVolume { get; set; }

    /// <summary>Type of transmission (AT, MT, CVT, etc.).</summary>
    public string TransmissionType { get; set; }

    /// <summary>Hourly rental rate in USD.</summary>
    public decimal HourlyRate { get; set; }
}
