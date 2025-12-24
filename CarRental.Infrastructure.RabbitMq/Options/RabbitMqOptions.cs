namespace CarRental.Infrastructure.RabbitMq.Options;

/// <summary>
/// Configuration options for RabbitMQ consumer
/// </summary>
public class RabbitMqOptions
{
    /// <summary>
    /// Configuration section name
    /// </summary>
    public const string SectionName = "RabbitMq";

    /// <summary>
    /// Queue name for rental contracts
    /// </summary>
    public string QueueName { get; set; } = "rental-contracts";

    /// <summary>
    /// Number of retry attempts for connection
    /// </summary>
    public int RetryCount { get; set; } = 5;

    /// <summary>
    /// Base delay between retries in milliseconds
    /// </summary>
    public int RetryDelayMs { get; set; } = 1000;
}
