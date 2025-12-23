using CarRental.Application.Contracts;
using CarRental.Application.Contracts.Dto;
using CarRental.Application.Profiles;
using CarRental.Application.Services;
using CarRental.Domain;
using CarRental.Domain.Data;
using CarRental.Domain.Models;
using CarRental.Infrastructure.EfCore;
using CarRental.Infrastructure.EfCore.Repositories;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddTransient<IRepository<Car>, CarRepository>();
builder.Services.AddTransient<IRepository<Client>, ClientRepository>();
builder.Services.AddTransient<IRepository<Model>, ModelRepository>();
builder.Services.AddTransient<IRepository<ModelGeneration>, ModelGenerationRepository>();
builder.Services.AddTransient<IRepository<Rental>, RentalRepository>();

builder.Services.AddScoped<IApplicationService<CarDto, CarCreateUpdateDto>, CarService>();
builder.Services.AddScoped<IApplicationService<ClientDto, ClientCreateUpdateDto>, ClientService>();
builder.Services.AddScoped<IApplicationService<ModelDto, ModelCreateUpdateDto>, ModelService>();
builder.Services.AddScoped<IApplicationService<ModelGenerationDto, ModelGenerationCreateUpdateDto>, ModelGenerationService>();
builder.Services.AddScoped<IApplicationService<RentalDto, RentalCreateUpdateDto>, RentalService>();

builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new CarRentalProfile());
});

builder.Services.AddControllers().AddJsonOptions(o =>
{
    o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var assemblies = AppDomain.CurrentDomain.GetAssemblies()
        .Where(a => a.GetName().Name!.StartsWith("CarRental"))
        .Distinct();

    foreach (var assembly in assemblies)
    {
        var xmlFile = $"{assembly.GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        if (File.Exists(xmlPath))
            c.IncludeXmlComments(xmlPath);
    }

    c.UseInlineDefinitionsForEnums();
});

builder.AddMongoDBClient("CarRentalConnection");

builder.Services.AddDbContext<CarRentalDbContext>((services, o) =>
{
    var db = services.GetRequiredService<IMongoDatabase>();
    o.UseMongoDB(db.Client, db.DatabaseNamespace.DatabaseName);
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<CarRentalDbContext>();

    var dataSeed = new DataSeed();

    if (!dbContext.Cars.Any())
    {
        foreach (var spec in dataSeed.Cars)
            await dbContext.Cars.AddAsync(spec);

        foreach (var doctor in dataSeed.Clients)
            await dbContext.Clients.AddAsync(doctor);

        foreach (var patient in dataSeed.Models)
            await dbContext.Models.AddAsync(patient);

        foreach (var appointment in dataSeed.Generations)
            await dbContext.ModelGenerations.AddAsync(appointment);

        foreach (var appointment in dataSeed.Rentals)
            await dbContext.Rentals.AddAsync(appointment);

        await dbContext.SaveChangesAsync();
    }
}

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();