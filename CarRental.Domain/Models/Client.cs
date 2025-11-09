namespace CarRental.Domain.Models;

/// <summary>
/// Represents a client who can rent cars.
/// </summary>
public class Client
{
    /// <summary>Driver's license number of the client.</summary>
    public string LicenseNumber { get; set; }

    /// <summary>Full name of the client.</summary>
    public string FullName { get; set; }

    /// <summary>Date of birth of the client.</summary>
    public DateTime BirthDate { get; set; }
}
