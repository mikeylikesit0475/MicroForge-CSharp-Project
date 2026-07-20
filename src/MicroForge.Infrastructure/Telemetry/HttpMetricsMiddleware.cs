using Microsoft.AspNetCore.Http;

namespace MicroForge.Infrastructure.Telemetry;

/// <summary>
/// Middleware that counts every HTTP request and every HTTP error (status >= 500)
/// for the /metrics endpoint (MF-034).
/// </summary>
public sealed class HttpMetricsMiddleware
{
    private readonly RequestDelegate _next;
    private readonly AppMetricsService _metrics;

    public HttpMetricsMiddleware(RequestDelegate next, AppMetricsService metrics)
    {
        _next = next;
        _metrics = metrics;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        _metrics.IncrementHttpRequests();

        try
        {
            await _next(context);
        }
        finally
        {
            if (context.Response.StatusCode >= 500)
                _metrics.IncrementHttpErrors();
        }
    }
}