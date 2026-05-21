using Prometheus;
using Serilog;
using Serilog.Context;


Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console(outputTemplate:
        "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} " +
        "{Properties:j}{NewLine}{Exception}")
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Logging.AddSerilog().SetMinimumLevel(LogLevel.Information);
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};
    
app.UseHttpMetrics();

app.MapMetrics();
Log.Logger.Information("21312");
app.Use(async (ctx, next) =>
{
    using (LogContext.PushProperty("TraceId", ctx.TraceIdentifier))
    using (LogContext.PushProperty("UserId", ctx.User.Identity?.Name))
    {
        await next();
    }
});
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
        Log.Logger.Information("Weather is @{@weather}", forecast);
        using (LogContext.PushProperty("A", 1))
        {
            Log.Logger.Information("Carries property A = 1");

            using (LogContext.PushProperty("A", 2))
            using (LogContext.PushProperty("B", 1))
            {
                Log.Logger.Information("Carries A = 2 and B = 1");
            }

            Log.Logger.Information("Carries property A = 1, again");
        }
        return forecast;
    })
    .WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}