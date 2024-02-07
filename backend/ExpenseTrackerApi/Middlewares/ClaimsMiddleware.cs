using Domain.ValidationAttributes;

namespace ExpenseTrackerApi.Middlewares
{
	public class ClaimsMiddleware(RequestDelegate next)
	{
		private readonly RequestDelegate _next = next;

		public async Task Invoke(HttpContext context)
		{
			var httpPath = context.Request.Path;

			var isAllowAnonymous = context.GetEndpoint()?.Metadata.GetMetadata<AllowAnonymousAttribute>() != null;

			if (isAllowAnonymous)
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
						context.Response.StatusCode = StatusCodes.Status401Unauthorized;
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
}
