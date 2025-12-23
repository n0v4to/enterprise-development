using CarRental.Domain.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;

namespace CarRental.Infrastructure.EfCore;

/// <summary>
/// Represents the database context for the Car Rental application.
/// </summary>
/// <param name="options">The options for this context.</param>
public class CarRentalDbContext(DbContextOptions<CarRentalDbContext> options) : DbContext(options)
{
    /// <summary>
    /// DbSet for Cars.
    /// </summary>
    public DbSet<Car> Cars { get; set; }

    /// <summary>
    /// DbSet for Clients.
    /// </summary>
    public DbSet<Client> Clients { get; set; }

    /// <summary>
    /// DbSet for Rentals.
    /// </summary>
    public DbSet<Rental> Rentals { get; set; }

    /// <summary>
    /// DbSet for Models.
    /// </summary>
    public DbSet<Model> Models { get; set; }

    /// <summary>
    /// DbSet for Model Generations.
    /// </summary>
    public DbSet<ModelGeneration> ModelGenerations { get; set; }

    /// <summary>
    /// Configures the model for the context.
    /// </summary>
    /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        Database.AutoTransactionBehavior = AutoTransactionBehavior.Never;

        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Car>(entity =>
        {
            entity.ToCollection("cars");
            entity.HasKey(e => e.Id);
            entity.Property(p => p.Id).HasElementName("_id");
            entity.Property(p => p.LicensePlate).HasElementName("license_plate").IsRequired().HasMaxLength(15);
            entity.Property(p => p.Color).HasElementName("color").IsRequired().HasMaxLength(30);
            entity.Property(p => p.GenerationId).HasElementName("generation_id").IsRequired();
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.ToCollection("clients");
            entity.HasKey(e => e.Id);
            entity.Property(p => p.Id).HasElementName("_id");
            entity.Property(p => p.LicenseNumber).HasElementName("license_number").IsRequired().HasMaxLength(20);
            entity.Property(p => p.FullName).HasElementName("full_name").IsRequired().HasMaxLength(100);
            entity.Property(p => p.BirthDate).HasElementName("birth_date").IsRequired();
        });

        modelBuilder.Entity<Rental>(entity =>
        {
            entity.ToCollection("rentals");
            entity.HasKey(e => e.Id);
            entity.Property(p => p.Id).HasElementName("_id");
            entity.Property(p => p.ClientId).HasElementName("client_id").IsRequired();
            entity.Property(p => p.CarId).HasElementName("car_id").IsRequired();
            entity.Property(p => p.StartTime).HasElementName("start_time").IsRequired();
            entity.Property(p => p.DurationHours).HasElementName("duration_hours").IsRequired();
            entity.Property(p => p.TotalCost).HasElementName("total_cost").IsRequired();
        });

        modelBuilder.Entity<Model>(entity =>
        {
            entity.ToCollection("models");
            entity.HasKey(e => e.Id);
            entity.Property(p => p.Id).HasElementName("_id");
            entity.Property(p => p.Name).HasElementName("name").IsRequired().HasMaxLength(50);
            entity.Property(p => p.DriveType).HasElementName("drive_type").IsRequired();
            entity.Property(p => p.Seats).HasElementName("seats").IsRequired();
            entity.Property(p => p.BodyType).HasElementName("body_type").IsRequired();
            entity.Property(p => p.Class).HasElementName("class").IsRequired();
        });

        modelBuilder.Entity<ModelGeneration>(entity =>
        {
            entity.ToCollection("model_generations");
            entity.HasKey(e => e.Id);
            entity.Property(p => p.Id).HasElementName("_id");
            entity.Property(p => p.ModelId).HasElementName("model_id").IsRequired();
            entity.Property(p => p.Year).HasElementName("year").IsRequired();
            entity.Property(p => p.EngineVolume).HasElementName("engine_volume").IsRequired();
            entity.Property(p => p.TransmissionType).HasElementName("transmission_type").IsRequired();
            entity.Property(p => p.HourlyRate).HasElementName("hourly_rate").IsRequired();
        });
    }
}