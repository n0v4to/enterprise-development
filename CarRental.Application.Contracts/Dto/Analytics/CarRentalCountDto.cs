namespace CarRental.Application.Contracts.Dto.Analytics;

using CarRental.Application.Contracts.Dto;

/// <summary>
/// DTO for rental count per car analytics.
/// </summary>
public class CarRentalCountDto
{
    /// <summary>
    /// Car details.
    /// </summary>
    public required CarDto Car { get; set; }

    /// <summary>
    /// Number of rentals for this car.
    /// </summary>
    public int RentalCount { get; set; }
}