namespace CarRental.Application.Contracts.Dto;

/// <summary>
/// Data transfer object for creating or updating a Rental entity.
/// </summary>
public class RentalCreateUpdateDto
{
    /// <summary>
    /// Id of the client who rented the car.
    /// </summary>
    public required int ClientId { get; set; }

    /// <summary>
    /// Id of the car that was rented.
    /// </summary>
    public required int CarId { get; set; }

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