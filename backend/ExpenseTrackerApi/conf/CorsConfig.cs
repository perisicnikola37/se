namespace ExpenseTrackerApi.conf;

public static class CorsConfig
{
	public static void ConfigureCors(this IServiceCollection services)
	{
		services.AddCors(options =>
		{
			options.AddPolicy("cors", corsPolicyBuilder =>
			{
				corsPolicyBuilder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
			});
		});
	}
}
