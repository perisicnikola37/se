using System.Net;
using Domain.Exceptions;
using Newtonsoft.Json;

public class GlobalExceptionHandlerMiddleware
{
	private readonly RequestDelegate _next;

	public GlobalExceptionHandlerMiddleware(RequestDelegate next)
	{
		_next = next ?? throw new ArgumentNullException(nameof(next));
	}

	public async Task Invoke(HttpContext context)
	{
		try
		{
			await _next(context);
		}
		catch (Exception ex)
		{
			await HandleExceptionAsync(context, ex);
		}
	}

	private static Task HandleExceptionAsync(HttpContext context, Exception exception)
	{
		var code = HttpStatusCode.InternalServerError;
		string? fileName = null;

		if (exception is InvalidAccountTypeException invalidAccountTypeException)
		{
			code = HttpStatusCode.BadRequest;
			fileName = invalidAccountTypeException.FileName;
		}

		var errorResponse = new ErrorResponse
		{
			Error = exception.Message,
			StatusCode = (int)code,
			Path = context.Request.Path,
			FileName = fileName
		};

		var result = JsonConvert.SerializeObject(errorResponse);
		context.Response.ContentType = "application/json";
		context.Response.StatusCode = (int)code;

		return context.Response.WriteAsync(result);
	}
}

// Extension method used to add the middleware to the HTTP request pipeline.
public static class GlobalExceptionHandlerMiddlewareExtensions
{
	public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder builder)
	{
		return builder.UseMiddleware<GlobalExceptionHandlerMiddleware>();
	}
}
