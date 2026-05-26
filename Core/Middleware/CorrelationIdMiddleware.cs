using Serilog.Context;

namespace Core.Middleware;

public class CorrelationIdMiddleware
{
    private readonly RequestDelegate _next;

    public CorrelationIdMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var name = "X-Correlation-ID";
        var id = context.Request.Headers[name].FirstOrDefault();
        if (string.IsNullOrEmpty(id))
        
        {
            id = Guid.NewGuid().ToString();
        }
        context.Response.Headers[name] = id;
        using (LogContext.PushProperty(name, id))
        {
            await _next.Invoke(context);
        }
    }
}