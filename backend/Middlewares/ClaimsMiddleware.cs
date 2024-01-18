public class ClaimsMiddleware
{
    private readonly RequestDelegate _next;

    public ClaimsMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var httpMethod = context.Request.Method;
        var httpPath = context.Request.Path;

        var excludedEndpoints = AuthenticationEndpointExclusions.ExcludedEndpoints;

        if (httpMethod == "GET" || excludedEndpoints.Contains(httpPath))
        {
            await _next(context);
        }
        else
        {
            var userIdClaim = context.User.Claims.FirstOrDefault(c => c.Type == "Id");

            if (userIdClaim == null)
            {
                if (!context.Response.HasStarted)
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Unauthorized: Invalid user claims");
                }
            }
            else
            {
                await _next(context);
            }
        }
    }
}
