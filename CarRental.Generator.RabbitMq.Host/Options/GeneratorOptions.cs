namespace CarRental.Generator.RabbitMq.Host.Options;

/// <summary>
/// Configuration options for rental contract generator
/// </summary>
public class GeneratorOptions
{
    /// <summary>
    /// Configuration section name
    /// </summary>
    public const string SectionName = "Generator";

    /// <summary>
    /// Minimum ClientId for generated rentals
    /// </summary>
    public int MinClientId { get; set; } = 1;

    /// <summary>
    /// Maximum ClientId for generated rentals
    /// </summary>
    public int MaxClientId { get; set; } = 10;

    /// <summary>
    /// Minimum CarId for generated rentals
    /// </summary>
    public int MinCarId { get; set; } = 1;

    /// <summary>
    /// Maximum CarId for generated rentals
    /// </summary>
    public int MaxCarId { get; set; } = 10;

    /// <summary>
    /// Minimum rental duration in hours
    /// </summary>
    public int MinDurationHours { get; set; } = 1;

    /// <summary>
    /// Maximum rental duration in hours
    /// </summary>
    public int MaxDurationHours { get; set; } = 24;

    /// <summary>
    /// Minimum hourly rate for cost calculation
    /// </summary>
    public decimal MinHourlyRate { get; set; } = 10;

    /// <summary>
    /// Maximum hourly rate for cost calculation
    /// </summary>
    public decimal MaxHourlyRate { get; set; } = 500;

    /// <summary>
    /// Maximum number of days in past for StartTime
    /// </summary>
    public int MaxDaysInPast { get; set; } = 30;
}