using System.Net;
using Newtonsoft.Json;
using Contracts.Dto;
using Domain.Exceptions;

namespace ExpenseTrackerApi.Middlewares
{
	public class GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
    {
        public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await next(context);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, ex.Message);
				await HandleExceptionAsync(context, ex);
			}
		}

		private static Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			var code = HttpStatusCode.InternalServerError;
			string? fileName = null;

			switch (exception)
			{
				case InvalidAccountTypeException invalidAccountTypeException:
					code = HttpStatusCode.BadRequest;
					fileName = invalidAccountTypeException.FileName;
					break;

				case ConflictException conflictException:
					code = HttpStatusCode.Conflict;
					fileName = conflictException.FileName;
					break;

				case DatabaseException databaseException:
					code = HttpStatusCode.InternalServerError;
					fileName = databaseException.FileName;
					break;

				case OnModelCreatingException onModelCreatingException:
					code = HttpStatusCode.InternalServerError;
					fileName = onModelCreatingException.FileName;
					break;
				case UnauthorizedException unauthorizedException:
					code = HttpStatusCode.Unauthorized;
					fileName = unauthorizedException.FileName;
					break;
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

	public static class GlobalExceptionHandlerMiddlewareExtensions
	{
		public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<GlobalExceptionHandlerMiddleware>();
		}
	}
}
