using System.Reflection;
using Application.Di;
using Application.Interfaces;
using Core;
using Infrastructure.Di;
using Prometheus;
using Serilog;
using Serilog.Context;
using Serilog.Sinks.PostgreSQL.ColumnWriters;

var builder = WebApplication.CreateBuilder(args);
Configuration.Configure(builder.Configuration);
//Serilog.Debugging.SelfLog.Enable(Console.Error);
builder.Host.UseSerilog();
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddInfastructure(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseRouting();
app.UseHttpsRedirection();
app.UseHttpMetrics();
app.MapMetrics();

app.MapControllers();
app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}