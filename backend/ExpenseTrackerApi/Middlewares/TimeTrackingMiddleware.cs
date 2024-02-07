using System.Diagnostics;

namespace ExpenseTrackerApi.Middlewares;

public class TimeTrackingMiddleware(RequestDelegate next, ILogger<TimeTrackingMiddleware> logger)
{
    public async Task Invoke(HttpContext context)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        await next(context);

        stopwatch.Stop();
        var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;

        logger.LogInformation($"Request {context.Request.Method} {context.Request.Path} took {elapsedMilliseconds} ms");
    }
}