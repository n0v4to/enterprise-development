using CarRental.Domain.Models;

namespace CarRental.Domain.Data;

/// <summary>
/// Represents a set of test data for car rentals.
/// </summary>
public class DataSeed
{
    /// <summary>List of car models.</summary>
    public List<Model> Models { get; }

    /// <summary>List of car model generations.</summary>
    public List<ModelGeneration> Generations { get; }

    /// <summary>List of cars available in the rental system.</summary>
    public List<Car> Cars { get; }

    /// <summary>List of clients who can rent cars.</summary>
    public List<Client> Clients { get; }

    /// <summary>List of car rentals performed by clients.</summary>
    public List<Rental> Rentals { get; }

    /// <summary>
    /// Initializes the test data collections with diverse data for cars, clients, generations, and rentals.
    /// </summary>
    public DataSeed()
    {
        Models = new List<Model>
        {
            new() { Name = "Toyota Corolla", DriveType = "FWD", Seats = 5, BodyType = "Sedan", Class = "C" },
            new() { Name = "Ford Focus", DriveType = "FWD", Seats = 5, BodyType = "Hatchback", Class = "C" },
            new() { Name = "BMW X5", DriveType = "AWD", Seats = 5, BodyType = "SUV", Class = "E" },
            new() { Name = "Kia Rio", DriveType = "FWD", Seats = 5, BodyType = "Sedan", Class = "B" },
            new() { Name = "Mercedes E-Class", DriveType = "RWD", Seats = 5, BodyType = "Sedan", Class = "E" },
            new() { Name = "Audi A4", DriveType = "AWD", Seats = 5, BodyType = "Sedan", Class = "D" },
            new() { Name = "Hyundai Tucson", DriveType = "AWD", Seats = 5, BodyType = "SUV", Class = "C" },
            new() { Name = "Volkswagen Golf", DriveType = "FWD", Seats = 5, BodyType = "Hatchback", Class = "C" },
            new() { Name = "Nissan Qashqai", DriveType = "AWD", Seats = 5, BodyType = "SUV", Class = "C" },
            new() { Name = "Honda Civic", DriveType = "FWD", Seats = 5, BodyType = "Sedan", Class = "B" }
        };

        Generations = new List<ModelGeneration>
        {
            new() { Model = Models[0], Year = 2020, EngineVolume = 1.6, TransmissionType = "AT", HourlyRate = 10 },
            new() { Model = Models[1], Year = 2019, EngineVolume = 1.8, TransmissionType = "MT", HourlyRate = 9 },
            new() { Model = Models[2], Year = 2021, EngineVolume = 3.0, TransmissionType = "AT", HourlyRate = 20 },
            new() { Model = Models[3], Year = 2022, EngineVolume = 1.4, TransmissionType = "CVT", HourlyRate = 8 },
            new() { Model = Models[4], Year = 2023, EngineVolume = 2.0, TransmissionType = "AT", HourlyRate = 25 },
            new() { Model = Models[5], Year = 2020, EngineVolume = 2.0, TransmissionType = "AT", HourlyRate = 15 },
            new() { Model = Models[6], Year = 2021, EngineVolume = 2.4, TransmissionType = "MT", HourlyRate = 12 },
            new() { Model = Models[7], Year = 2018, EngineVolume = 1.6, TransmissionType = "CVT", HourlyRate = 9 },
            new() { Model = Models[8], Year = 2019, EngineVolume = 2.0, TransmissionType = "AT", HourlyRate = 13 },
            new() { Model = Models[9], Year = 2022, EngineVolume = 1.5, TransmissionType = "MT", HourlyRate = 8 }
        };

        Cars = new List<Car>
        {
            new() { LicensePlate = "A111AA", Color = "Red", Generation = Generations[0] },
            new() { LicensePlate = "B222BB", Color = "Blue", Generation = Generations[1] },
            new() { LicensePlate = "C333CC", Color = "Black", Generation = Generations[2] },
            new() { LicensePlate = "D444DD", Color = "White", Generation = Generations[3] },
            new() { LicensePlate = "E555EE", Color = "Silver", Generation = Generations[4] },
            new() { LicensePlate = "F666FF", Color = "Green", Generation = Generations[5] },
            new() { LicensePlate = "G777GG", Color = "Yellow", Generation = Generations[6] },
            new() { LicensePlate = "H888HH", Color = "Gray", Generation = Generations[7] },
            new() { LicensePlate = "I999II", Color = "Blue", Generation = Generations[8] },
            new() { LicensePlate = "J000JJ", Color = "Black", Generation = Generations[9] }
        };

        Clients = new List<Client>
        {
            new() { LicenseNumber = "1111", FullName = "Ivan Petrov", BirthDate = new DateTime(1990, 5, 12) },
            new() { LicenseNumber = "2222", FullName = "Olga Sidorova", BirthDate = new DateTime(1985, 8, 21) },
            new() { LicenseNumber = "3333", FullName = "Pavel Smirnov", BirthDate = new DateTime(1993, 1, 3) },
            new() { LicenseNumber = "4444", FullName = "Elena Ivanova", BirthDate = new DateTime(1999, 12, 14) },
            new() { LicenseNumber = "5555", FullName = "Alexey Volk", BirthDate = new DateTime(1988, 7, 25) },
            new() { LicenseNumber = "6666", FullName = "Maria Kuznetsova", BirthDate = new DateTime(1992, 3, 17) },
            new() { LicenseNumber = "7777", FullName = "Sergey Orlov", BirthDate = new DateTime(1987, 10, 30) },
            new() { LicenseNumber = "8888", FullName = "Anna Fedorova", BirthDate = new DateTime(1995, 2, 5) },
            new() { LicenseNumber = "9999", FullName = "Dmitry Pavlov", BirthDate = new DateTime(1991, 11, 12) },
            new() { LicenseNumber = "1010", FullName = "Irina Morozova", BirthDate = new DateTime(1989, 9, 9) }
        };

        Rentals = new List<Rental>
        {
            new() { Client = Clients[0], Car = Cars[0], StartTime = DateTime.Now.AddHours(-10), DurationHours = 3 },
            new() { Client = Clients[1], Car = Cars[0], StartTime = DateTime.Now.AddHours(-50), DurationHours = 5 },
            new() { Client = Clients[2], Car = Cars[0], StartTime = DateTime.Now.AddHours(-120), DurationHours = 2 },
            new() { Client = Clients[3], Car = Cars[0], StartTime = DateTime.Now.AddHours(-200), DurationHours = 4 },
            new() { Client = Clients[5], Car = Cars[1], StartTime = DateTime.Now.AddHours(-70), DurationHours = 3 },
            new() { Client = Clients[6], Car = Cars[2], StartTime = DateTime.Now.AddHours(-40), DurationHours = 4 },
            new() { Client = Clients[7], Car = Cars[2], StartTime = DateTime.Now.AddHours(-150), DurationHours = 5 },
            new() { Client = Clients[8], Car = Cars[3], StartTime = DateTime.Now.AddHours(-25), DurationHours = 4 },
            new() { Client = Clients[9], Car = Cars[3], StartTime = DateTime.Now.AddHours(-200), DurationHours = 7 },
            new() { Client = Clients[0], Car = Cars[4], StartTime = DateTime.Now.AddHours(-300), DurationHours = 5 },
            new() { Client = Clients[2], Car = Cars[5], StartTime = DateTime.Now.AddHours(-400), DurationHours = 3 },
            new() { Client = Clients[1], Car = Cars[6], StartTime = DateTime.Now.AddHours(-500), DurationHours = 2 },
            new() { Client = Clients[3], Car = Cars[7], StartTime = DateTime.Now.AddHours(-600), DurationHours = 4 },
            new() { Client = Clients[5], Car = Cars[1], StartTime = DateTime.Now.AddHours(-20), DurationHours = 2 },
            new() { Client = Clients[6], Car = Cars[0], StartTime = DateTime.Now.AddHours(-90), DurationHours = 3 },
            new() { Client = Clients[7], Car = Cars[2], StartTime = DateTime.Now.AddHours(-60), DurationHours = 2 },
            new() { Client = Clients[8], Car = Cars[3], StartTime = DateTime.Now.AddHours(-150), DurationHours = 5 },
            new() { Client = Clients[9], Car = Cars[1], StartTime = DateTime.Now.AddHours(-70), DurationHours = 3 }
        };
    }
}
