namespace CarRental.Application.Contracts.Dto;

/// <summary>
/// Data transfer object for Client entity.
/// </summary>
public class ClientDto
{
    /// <summary>
    /// Unique identifier of the client.
    /// </summary>
    public int Id { get; set; }

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