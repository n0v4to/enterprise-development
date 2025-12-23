namespace CarRental.Application.Contracts.Dto;

/// <summary>
/// Data transfer object for creating or updating a Client entity.
/// </summary>
public class ClientCreateUpdateDto
{
    /// <summary>
    /// Driver's license number of the client.
    /// </summary>
    public required string LicenseNumber { get; set; }

    /// <summary>
    /// Full name of the client.
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Date of birth of the client.
    /// </summary>
    public required DateTime BirthDate { get; set; }
}