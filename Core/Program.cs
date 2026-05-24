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
builder.Host.UseSerilog();
builder.Logging.AddSerilog().SetMinimumLevel(LogLevel.Information);
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

app.UseHttpsRedirection();
//app.UseHttpMetrics();
//app.MapMetrics();
// app.Use(async (ctx, next) =>
// {
//     using (LogContext.PushProperty("TraceId", ctx.TraceIdentifier))
//     using (LogContext.PushProperty("UserId", ctx.User.Identity?.Name))
//     {
//         await next();
//     }
// });
app.UseRouting();
app.MapControllers();
app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}