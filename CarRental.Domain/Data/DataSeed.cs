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

    private readonly DateTime _baseDate = new(2025, 11, 28, 12, 0, 0);

    /// <summary>
    /// Initializes the test data collections with diverse data for cars, clients, generations, and rentals.
    /// </summary>
    public DataSeed()
    {
        Models = [
            new() { Id = 1, Name = "Toyota Corolla", DriveType = CarRental.Domain.Models.DriveType.Fwd, Seats = 5, BodyType = BodyType.Sedan, Class = CarClass.C },
            new() { Id = 2, Name = "Ford Focus", DriveType = CarRental.Domain.Models.DriveType.Fwd, Seats = 5, BodyType = BodyType.Hatchback, Class = CarClass.C },
            new() { Id = 3, Name = "BMW X5", DriveType = CarRental.Domain.Models.DriveType.Awd, Seats = 5, BodyType = BodyType.Suv, Class = CarClass.E },
            new() { Id = 4, Name = "Kia Rio", DriveType = CarRental.Domain.Models.DriveType.Fwd, Seats = 5, BodyType = BodyType.Sedan, Class = CarClass.B },
            new() { Id = 5, Name = "Mercedes E-Class", DriveType = CarRental.Domain.Models.DriveType.Rwd, Seats = 5, BodyType = BodyType.Sedan, Class = CarClass.E },
            new() { Id = 6, Name = "Audi A4", DriveType = CarRental.Domain.Models.DriveType.Awd, Seats = 5, BodyType = BodyType.Sedan, Class = CarClass.D },
            new() { Id = 7, Name = "Hyundai Tucson", DriveType = CarRental.Domain.Models.DriveType.Awd, Seats = 5, BodyType = BodyType.Suv, Class = CarClass.C },
            new() { Id = 8, Name = "Volkswagen Golf", DriveType = CarRental.Domain.Models.DriveType.Fwd, Seats = 5, BodyType = BodyType.Hatchback, Class = CarClass.C },
            new() { Id = 9, Name = "Nissan Qashqai", DriveType = CarRental.Domain.Models.DriveType.Awd, Seats = 5, BodyType = BodyType.Suv, Class = CarClass.C },
            new() { Id = 10, Name = "Honda Civic", DriveType = CarRental.Domain.Models.DriveType.Fwd, Seats = 5, BodyType = BodyType.Sedan, Class = CarClass.B }
        ];

        Generations = [
            new() { Id = 1, ModelId = 1, Year = 2020, EngineVolume = 1.6, TransmissionType = TransmissionType.At, HourlyRate = 10 },
            new() { Id = 2, ModelId = 2, Year = 2019, EngineVolume = 1.8, TransmissionType = TransmissionType.Mt, HourlyRate = 9 },
            new() { Id = 3, ModelId = 3, Year = 2021, EngineVolume = 3.0, TransmissionType = TransmissionType.At, HourlyRate = 20 },
            new() { Id = 4, ModelId = 4, Year = 2022, EngineVolume = 1.4, TransmissionType = TransmissionType.Cvt, HourlyRate = 8 },
            new() { Id = 5, ModelId = 5, Year = 2023, EngineVolume = 2.0, TransmissionType = TransmissionType.At, HourlyRate = 25 },
            new() { Id = 6, ModelId = 6, Year = 2020, EngineVolume = 2.0, TransmissionType = TransmissionType.At, HourlyRate = 15 },
            new() { Id = 7, ModelId = 7, Year = 2021, EngineVolume = 2.4, TransmissionType = TransmissionType.Mt, HourlyRate = 12 },
            new() { Id = 8, ModelId = 8, Year = 2018, EngineVolume = 1.6, TransmissionType = TransmissionType.Cvt, HourlyRate = 9 },
            new() { Id = 9, ModelId = 9, Year = 2019, EngineVolume = 2.0, TransmissionType = TransmissionType.At, HourlyRate = 13 },
            new() { Id = 10, ModelId = 10, Year = 2022, EngineVolume = 1.5, TransmissionType = TransmissionType.Mt, HourlyRate = 8 }
        ];

        Cars = [
            new() { Id = 1, LicensePlate = "A111AA", Color = "Red", GenerationId = 1 },
            new() { Id = 2, LicensePlate = "B222BB", Color = "Blue", GenerationId = 2 },
            new() { Id = 3, LicensePlate = "C333CC", Color = "Black", GenerationId = 3 },
            new() { Id = 4, LicensePlate = "D444DD", Color = "White", GenerationId = 4 },
            new() { Id = 5, LicensePlate = "E555EE", Color = "Silver", GenerationId = 5 },
            new() { Id = 6, LicensePlate = "F666FF", Color = "Green", GenerationId = 6 },
            new() { Id = 7, LicensePlate = "G777GG", Color = "Yellow", GenerationId = 7 },
            new() { Id = 8, LicensePlate = "H888HH", Color = "Gray", GenerationId = 8 },
            new() { Id = 9, LicensePlate = "I999II", Color = "Blue", GenerationId = 9 },
            new() { Id = 10, LicensePlate = "J000JJ", Color = "Black", GenerationId = 10 }
        ];

        Clients = [
            new() { Id = 1, LicenseNumber = "1111", FullName = "Ivan Petrov", BirthDate = new DateTime(1990, 5, 12) },
            new() { Id = 2, LicenseNumber = "2222", FullName = "Olga Sidorova", BirthDate = new DateTime(1985, 8, 21) },
            new() { Id = 3, LicenseNumber = "3333", FullName = "Pavel Smirnov", BirthDate = new DateTime(1993, 1, 3) },
            new() { Id = 4, LicenseNumber = "4444", FullName = "Elena Ivanova", BirthDate = new DateTime(1999, 12, 14) },
            new() { Id = 5, LicenseNumber = "5555", FullName = "Alexey Volk", BirthDate = new DateTime(1988, 7, 25) },
            new() { Id = 6, LicenseNumber = "6666", FullName = "Maria Kuznetsova", BirthDate = new DateTime(1992, 3, 17) },
            new() { Id = 7, LicenseNumber = "7777", FullName = "Sergey Orlov", BirthDate = new DateTime(1987, 10, 30) },
            new() { Id = 8, LicenseNumber = "8888", FullName = "Anna Fedorova", BirthDate = new DateTime(1995, 2, 5) },
            new() { Id = 9, LicenseNumber = "9999", FullName = "Dmitry Pavlov", BirthDate = new DateTime(1991, 11, 12) },
            new() { Id = 10, LicenseNumber = "1010", FullName = "Irina Morozova", BirthDate = new DateTime(1989, 9, 9) }
        ];

        Rentals = [
            new() { Id = 1, ClientId = 1, CarId = 1, StartTime = _baseDate.AddHours(-2), DurationHours = 5, TotalCost = 50 },
            new() { Id = 2, ClientId = 2, CarId = 2, StartTime = _baseDate.AddHours(-1), DurationHours = 6, TotalCost = 54 },
            new() { Id = 3, ClientId = 3, CarId = 3, StartTime = _baseDate.AddHours(-3), DurationHours = 4, TotalCost = 80 },
            new() { Id = 4, ClientId = 4, CarId = 4, StartTime = _baseDate.AddHours(-2), DurationHours = 3, TotalCost = 24 },
            new() { Id = 5, ClientId = 5, CarId = 1, StartTime = _baseDate.AddHours(-20), DurationHours = 3, TotalCost = 30 },
            new() { Id = 6, ClientId = 6, CarId = 1, StartTime = _baseDate.AddHours(-50), DurationHours = 5, TotalCost = 50 },
            new() { Id = 7, ClientId = 7, CarId = 2, StartTime = _baseDate.AddHours(-30), DurationHours = 4, TotalCost = 36 },
            new() { Id = 8, ClientId = 8, CarId = 5, StartTime = _baseDate.AddHours(-15), DurationHours = 5, TotalCost = 125 },
            new() { Id = 9, ClientId = 7, CarId = 6, StartTime = _baseDate.AddHours(-40), DurationHours = 10, TotalCost = 150 },
            new() { Id = 10, ClientId = 8, CarId = 7, StartTime = _baseDate.AddHours(-50), DurationHours = 12, TotalCost = 144 },
            new() { Id = 11, ClientId = 3, CarId = 3, StartTime = _baseDate.AddHours(-60), DurationHours = 8, TotalCost = 160 },
            new() { Id = 12, ClientId = 1, CarId = 6, StartTime = _baseDate.AddHours(-80), DurationHours = 6, TotalCost = 90 },
            new() { Id = 13, ClientId = 4, CarId = 7, StartTime = _baseDate.AddHours(-100), DurationHours = 6, TotalCost = 72 }
        ];
    }
}