namespace CarRental.Application.Contracts.Dto.Analytics;

using CarRental.Application.Contracts.Dto;

/// <summary>
/// DTO for client total spent analytics.
/// </summary>
public class ClientTotalSpentDto
{
    /// <summary>
    /// Client details.
    /// </summary>
    public required ClientDto Client { get; set; }

    /// <summary>
    /// Total amount spent by the client on rentals.
    /// </summary>
    public decimal TotalSpent { get; set; }
}