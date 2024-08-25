using InvestWebAPI.Intefaces;
using InvestWebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(); // Add this to register MVC controllers
builder.Services.AddEndpointsApiExplorer(); // Register the OpenAPI/Swagger service
builder.Services.AddSwaggerGen(); // Register Swagger generator

//Error: neu dang ky nhu vay th� se tra ra null
//builder.Services.AddTransient<IShippingService, FedexShippingService>();
//builder.Services.AddTransient<IShippingService, CarShippingService>();
//Correct
builder.Services.AddTransient<FedexShippingService>();
builder.Services.AddTransient<CarShippingService>();
builder.Services.AddSingleton<IShippingServiceFactory, ShippingServiceFactory>();
//DI for constructor
builder.Services.AddTransient<IShippingService, CarShippingService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Enable Swagger middleware
    app.UseSwaggerUI(); // Enable Swagger UI middleware
}

app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers(); // Map the controllers

// Example endpoint
var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi(); // Adds OpenAPI documentation

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}





