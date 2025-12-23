using CarRental.Domain.Models;

namespace CarRental.Application.Contracts.Dto;

/// <summary>
/// Data transfer object for creating or updating a ModelGeneration entity.
/// </summary>
public class ModelGenerationCreateUpdateDto
{
    /// <summary>
    /// Id of the model to which this generation belongs.
    /// </summary>
    public required int ModelId { get; set; }

    /// <summary>
    /// Year of manufacture.
    /// </summary>
    public required int Year { get; set; }

    /// <summary>
    /// Engine volume in liters.
    /// </summary>
    public required double EngineVolume { get; set; }

    /// <summary>
    /// Type of transmission (AT, MT, CVT, etc.).
    /// </summary>
    public required TransmissionType TransmissionType { get; set; }

    /// <summary>
    /// Hourly rental rate in USD.
    /// </summary>
    public required decimal HourlyRate { get; set; }
}