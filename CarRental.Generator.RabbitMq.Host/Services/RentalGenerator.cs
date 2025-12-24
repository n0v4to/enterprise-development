using Bogus;
using CarRental.Application.Contracts.Dto;
using CarRental.Generator.RabbitMq.Host.Options;
using Microsoft.Extensions.Options;

namespace CarRental.Generator.RabbitMq.Host.Services;

/// <summary>
/// Service for generating random rental contracts using Bogus.
/// </summary>
public class RentalGenerator(IOptions<GeneratorOptions> options)
{
    private readonly Faker<RentalCreateUpdateDto> _faker = new Faker<RentalCreateUpdateDto>("ru")
            .RuleFor(r => r.ClientId, f => f.Random.Int(options.Value.MinClientId, options.Value.MaxClientId))
            .RuleFor(r => r.CarId, f => f.Random.Int(options.Value.MinCarId, options.Value.MaxCarId))
            .RuleFor(r => r.StartTime, f => f.Date.Recent(options.Value.MaxDaysInPast))
            .RuleFor(r => r.DurationHours, f => f.Random.Int(options.Value.MinDurationHours, options.Value.MaxDurationHours))
            .RuleFor(r => r.TotalCost, (f, r) =>
            {
                var rate = f.Random.Decimal(options.Value.MinHourlyRate, options.Value.MaxHourlyRate);
                return Math.Round(r.DurationHours * rate, 2);
            });

    /// <summary>
    /// Generates multiple random rental contracts.
    /// </summary>
    /// <param name="count">Number of contracts to generate.</param>
    /// <returns>A collection of generated rental DTOs.</returns>
    public IEnumerable<RentalCreateUpdateDto> Generate(int count)
    {
        return _faker.Generate(count);
    }
}