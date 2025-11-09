using System;
using System.Linq;
using CarRental.Domain.Data;

namespace CarRental.App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Все аренды клиентов ===\n");
            foreach (var rental in DataSeed.Rentals)
            {
                Console.WriteLine($"{rental.Client.FullName} rented {rental.Car.Generation.Model.Name} " +
                                  $"({rental.Car.LicensePlate}) for {rental.DurationHours}h, total cost: {rental.TotalCost} USD");
            }

            Console.WriteLine("\n=== Топ 5 наиболее часто арендованных автомобилей ===");
            var topCars = DataSeed.Rentals
                .GroupBy(r => r.Car)
                .OrderByDescending(g => g.Count())
                .Take(5)
                .Select(g => $"{g.Key.LicensePlate} — {g.Key.Generation.Model.Name}, {g.Count()} rentals");

            foreach (var line in topCars)
                Console.WriteLine(line);

            Console.WriteLine("\n=== Число аренд по каждому автомобилю ===");
            var rentalCounts = DataSeed.Rentals
                .GroupBy(r => r.Car)
                .Select(g => $"{g.Key.LicensePlate} — {g.Count()} rentals");

            foreach (var line in rentalCounts)
                Console.WriteLine(line);

            Console.WriteLine("\n=== Топ 5 клиентов по сумме аренды ===");
            var topClients = DataSeed.Rentals
                .GroupBy(r => r.Client)
                .Select(g => new { g.Key.FullName, TotalSpent = g.Sum(r => r.TotalCost) })
                .OrderByDescending(x => x.TotalSpent)
                .Take(5)
                .Select(x => $"{x.FullName} — Total spent: {x.TotalSpent} USD");

            foreach (var line in topClients)
                Console.WriteLine(line);

            Console.WriteLine("\n=== Все клиенты, которые арендовали Toyota Corolla (по ФИО) ===");
            var corollaClients = DataSeed.Rentals
                .Where(r => r.Car.Generation.Model.Name == "Toyota Corolla")
                .Select(r => r.Client)
                .Distinct()
                .OrderBy(c => c.FullName)
                .Select(c => c.FullName);

            foreach (var name in corollaClients)
                Console.WriteLine(name);

            Console.WriteLine("\n=== Конец проверки ===");
        }
    }
}
