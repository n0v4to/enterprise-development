namespace CarRental.Application.Contracts.Dto;

/// <summary>
/// Data transfer object for Rental entity.
/// </summary>
public class RentalDto
{
    /// <summary>
    /// Unique identifier of the rental.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Full name of the client who rented the car.
    /// </summary>
    public required string ClientFullName { get; set; }

    /// <summary>
    /// License plate of the rented car.
    /// </summary>
    public required string CarLicensePlate { get; set; }

    /// <summary>
    /// Start time of the rental.
    /// </summary>
    public required DateTime StartTime { get; set; }

    /// <summary>
    /// Duration of the rental in hours.
    /// </summary>
    public required int DurationHours { get; set; }

    /// <summary>
    /// Total cost of the rental.
    /// </summary>
    public required decimal TotalCost { get; set; }
}