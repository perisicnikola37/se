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

		if (httpMethod == "PUT" || httpMethod == "POST" || httpMethod == "PATCH" || httpMethod == "DELETE")
		{
			var userIdClaim = context.User.Claims.FirstOrDefault(c => c.Type == "Id");

			if (userIdClaim == null)
			{
				if (!context.Response.HasStarted)
				{
					context.Response.StatusCode = 401;
					await context.Response.WriteAsync("Unauthorized: Invalid user claims");
				}
				return;
			}
			else
			{
				await _next(context);
			}
		}
		else
		{
			await _next(context);
		}
	}
}

public static class ClaimsMiddlewareExtensions
{
	public static IApplicationBuilder UseClaimsMiddleware(this IApplicationBuilder builder)
	{
		return builder.UseMiddleware<ClaimsMiddleware>();
	}
}
