using ExpenseTrackerApi.Exclusions;

namespace ExpenseTrackerApi.Middlewares;

public class ClaimsMiddleware(RequestDelegate next, ILogger<ClaimsMiddleware> logger)
{
    public async Task Invoke(HttpContext context)
    {
        var httpPath = context.Request.Path;

        var excludedEndpoints = AuthenticationEndpointExclusions.ExcludedEndpoints;

        if (excludedEndpoints.Contains(httpPath))
        {
            await next(context);
        }
        else
        {
            var userIdClaim = context.User.Claims.FirstOrDefault(c => c.Type == "Id");

            if (userIdClaim == null)
            {
                if (!context.Response.HasStarted)
                {
                    logger.LogWarning("Unauthorized: Invalid user claims");
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Unauthorized: Invalid user claims");
                }
            }
            else
            {
                await next(context);
            }
        }
    }
}