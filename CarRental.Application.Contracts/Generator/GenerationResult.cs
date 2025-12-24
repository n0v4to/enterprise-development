namespace CarRental.Application.Contracts.Generator;

/// <summary>
/// Result of contract generation operation
/// </summary>
public class GenerationResult
{
    /// <summary>
    /// Number of contracts requested to generate
    /// </summary>
    public int RequestedCount { get; set; }

    /// <summary>
    /// Number of contracts successfully published
    /// </summary>
    public int PublishedCount { get; set; }

    /// <summary>
    /// Result message
    /// </summary>
    public string Message { get; set; } = string.Empty;
}