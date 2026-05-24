using Serilog;
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
            { "timestamp", new TimestampColumnWriter() },
            { "level", new LevelColumnWriter() },
            { "message", new RenderedMessageColumnWriter() },
            { "message_template", new MessageTemplateColumnWriter() },
            { "exception", new ExceptionColumnWriter() },
            { "properties", new PropertiesColumnWriter() }
        };
        var logTableName = config.GetSection("Logs").GetSection("tableName").Value;
        Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.Console(outputTemplate:
                "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} " +
                "{Properties:j}{NewLine}{Exception}")
            .WriteTo.PostgreSQL(config.GetConnectionString("LogContext"), logTableName, columnWriters)
            .CreateLogger();
    }
}