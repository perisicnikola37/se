public class UserMiddleware
{
    private readonly RequestDelegate _next;

    public UserMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        Console.WriteLine("Middleware: Logging message to console.");

        if (context.User.Identity.IsAuthenticated)
        {
            Console.WriteLine("Middleware: User is authenticated.");

            var userIdClaim = context.User.Claims.FirstOrDefault(c => c.Type == "Id");
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                context.Items["UserId"] = userId;
            }
        }
        else
        {
            Console.WriteLine("Middleware: User is not authenticated.");
        }

        await _next(context);
    }
}
