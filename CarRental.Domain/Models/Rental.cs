namespace CarRental.Domain.Models;

/// <summary>
/// Represents a car rental by a client.
/// </summary>
public class Rental
{
    public int Id { get; set; }

    /// <summary>Client who rented the car.</summary>
    public required Client Client { get; set; }

    /// <summary>Car that was rented.</summary>
    public required Car Car { get; set; }

    /// <summary>Start time of the rental.</summary>
    public required DateTime StartTime { get; set; }

    /// <summary>Duration of the rental in hours.</summary>
    public required int DurationHours { get; set; }

    /// <summary>Total cost of the rental (HourlyRate * DurationHours).</summary>
    public decimal TotalCost => Car.Generation.HourlyRate * DurationHours;
}