var builder = DistributedApplication.CreateBuilder(args);

var db = builder.AddMongoDB("mongo-container").AddDatabase("mongo-db");

var apiHost = builder.AddProject<Projects.CarRental_Api_Host>("car-rental-api-host")
    .WithReference(db, "CarRentalConnection")
    .WaitFor(db);

builder.Build().Run();