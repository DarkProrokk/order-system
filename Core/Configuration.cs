using Serilog;
using Serilog.Context;
using Serilog.Enrichers.Span;
using Serilog.Filters;
using Serilog.Sinks.PostgreSQL;
using Serilog.Sinks.PostgreSQL.ColumnWriters;

namespace Core;

public static class Configuration
{
    public static void Configure(IConfiguration configuration)
    {
        ConfigureLogs(configuration);
    }

    public static void ConfigureLogs(IConfiguration config)
    {
      
        IDictionary<string, ColumnWriterBase> columnWriters = new Dictionary<string, ColumnWriterBase>
        {
            {"id", new IdAutoIncrementColumnWriter()},
            { "timestamp", new TimestampColumnWriter() },
            { "level", new LevelColumnWriter() },
            { "message", new RenderedMessageColumnWriter() },
            { "message_template", new MessageTemplateColumnWriter() },
            { "exception", new ExceptionColumnWriter() },
            { "properties", new PropertiesColumnWriter() },
            { "trace_id", new SinglePropertyColumnWriter("TraceId", PropertyWriteMethod.Raw) },
            { "span_id", new SinglePropertyColumnWriter("SpanId", PropertyWriteMethod.Raw) }
        };
        var logTableName = config.GetSection("Logs").GetSection("tableName").Value;
        Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .Enrich.WithSpan()
            .Filter.ByExcluding(Matching.WithProperty<string>("RequestPath", p =>
                p.StartsWith("/metrics")))
            .WriteTo.Console(outputTemplate:
                "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} " +
                "{Properties:j}{NewLine}{Exception}")
            .WriteTo.PostgreSQL(config.GetConnectionString("LogContext"), logTableName, columnWriters, needAutoCreateTable: true, needAutoCreateSchema: true)
            .CreateLogger();
    }
}