namespace CarRental.Domain.Models;

/// <summary>
/// Represents a client who can rent cars.
/// </summary>
public class Client
{
    public int Id { get; set; }

    /// <summary>Driver's license number of the client.</summary>
    public required string LicenseNumber { get; set; }

    /// <summary>Full name of the client.</summary>
    public required string FullName { get; set; }

    /// <summary>Date of birth of the client.</summary>
    public required DateTime BirthDate { get; set; }
}