using ExpenseTrackerApi.conf;
using ExpenseTrackerApi.Middlewares;

public static class AppConfig
{
	public static void ConfigureApp(this WebApplication app)
	{
		app.ApplyDatabaseMigrations();
		app.ApplyRedirection();

		// app.UseHttpLogging();

		// handle default route path - redirection to swagger documentation

		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI();
		}

		app.UseHttpsRedirection();

		// Cors had to be before auth
		app.UseCors("cors");

		// auth
		app.UseAuthentication();
		app.UseAuthorization();

		// app.UseRateLimiter();

		app.MapControllers();

		// this middleware needs to be after .net auth middlewares!
		app.UseMiddleware<ClaimsMiddleware>();
		app.UseGlobalExceptionHandler();
		app.UseMiddleware<TimeTrackingMiddleware>();

		app.Run();
	}
}
