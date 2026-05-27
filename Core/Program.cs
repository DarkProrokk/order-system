using System.Reflection;
using Application.Di;
using Application.Interfaces;
using Core;
using Core.Middleware;
using Infrastructure.Di;
using OpenTelemetry.Exporter;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Prometheus;
using Serilog;
using Serilog.Context;
using Serilog.Sinks.PostgreSQL.ColumnWriters;

var builder = WebApplication.CreateBuilder(args);
Configuration.Configure(builder.Configuration);
Serilog.Debugging.SelfLog.Enable(Console.Error);
builder.Host.UseSerilog();
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddInfastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddOpenTelemetry()
    .WithTracing(tracing =>
    {
        tracing
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddEntityFrameworkCoreInstrumentation()
            //.AddConsoleExporter()
            .AddOtlpExporter(opt =>
            {
                opt.Endpoint = new Uri("http://localhost:4317");
                opt.Protocol = OtlpExportProtocol.Grpc;
            })
            .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(serviceName:"OrderService"));
    });
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


//  async Task DoWork(HttpContext context, Func<HttpContext, Task> next)
// {
//     using (LogContext.PushProperty("123", "123"))
//     {
//         await next();
//     }
//     
// }
 async Task DoWork2(HttpContext context, RequestDelegate @delegate)
 {

     
     using (LogContext.PushProperty("123", "123"))
     {
         await @delegate.Invoke(context);
     }

 }
record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}