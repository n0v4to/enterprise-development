using CarRental.Generator.RabbitMq.Host.Options;
using CarRental.Generator.RabbitMq.Host.Services;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddRabbitMQClient("rabbitMqConnection");

builder.Services.Configure<RabbitMqOptions>(builder.Configuration.GetSection(RabbitMqOptions.SectionName));
builder.Services.Configure<GeneratorOptions>(builder.Configuration.GetSection(GeneratorOptions.SectionName));

builder.Services.AddSingleton<RentalGenerator>();
builder.Services.AddSingleton<RabbitMqProducer>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.MapDefaultEndpoints();

app.Run();