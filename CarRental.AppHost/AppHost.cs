var builder = DistributedApplication.CreateBuilder(args);

var db = builder.AddMongoDB("mongo-container").AddDatabase("mongo-db");

var rabbitmq = builder.AddRabbitMQ("rabbitMqConnection")
    .WithManagementPlugin();

builder.AddProject<Projects.CarRental_Api_Host>("car-rental-api-host")
    .WithReference(db, "CarRentalConnection")
    .WithReference(rabbitmq)
    .WaitFor(db)
    .WaitFor(rabbitmq);

var generator = builder.AddProject<Projects.CarRental_Generator_RabbitMq_Host>("car-rental-generator")
    .WithReference(rabbitmq)
    .WaitFor(rabbitmq);

builder.Build().Run();