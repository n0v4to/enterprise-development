namespace CarRental.Domain.Models;

/// <summary>
/// Represents a specific generation of a car model.
/// </summary>
public class ModelGeneration
{
    public int Id { get; set; }

    /// <summary>Id of the model to which this generation belongs.</summary>
    public required int ModelId { get; set; }

    /// <summary>Year of manufacture.</summary>
    public required int Year { get; set; }

    /// <summary>Engine volume in liters.</summary>
    public required double EngineVolume { get; set; }

    /// <summary>Type of transmission (AT, MT, CVT, etc.).</summary>
    public required TransmissionType TransmissionType { get; set; }

    /// <summary>Hourly rental rate in USD.</summary>
    public required decimal HourlyRate { get; set; }
}