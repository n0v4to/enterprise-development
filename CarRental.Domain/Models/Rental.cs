namespace CarRental.Domain.Models;

/// <summary>
/// Represents a car rental by a client.
/// </summary>
public class Rental
{
    public int Id { get; set; }

    /// <summary>Id of the client who rented the car.</summary>
    public required int ClientId { get; set; }

    /// <summary>Id of the car that was rented.</summary>
    public required int CarId { get; set; }

    /// <summary>Start time of the rental.</summary>
    public required DateTime StartTime { get; set; }

    /// <summary>Duration of the rental in hours.</summary>
    public required int DurationHours { get; set; }

    /// <summary>Total cost of the rental.</summary>
    public required decimal TotalCost { get; set; }
}