namespace ExpenseTrackerApi.conf;

using DinkToPdf;
using DinkToPdf.Contracts;
using Presentation;

public static class BuilderConfig
{
	public static WebApplicationBuilder ConfigureBuilder(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);
		ConfigureServices(builder);
		return builder;
	}

	private static void ConfigureServices(WebApplicationBuilder builder)
	{
		var configuration = builder.Configuration;

		// Your existing service configurations
		builder.Services.ConfigureCors();
		builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
		builder.Services.AddScoped<PdfGenerator>();
		builder.Services.AddAuthorization();

		builder.Services.AddHttpContextAccessor();

		// Policies
		builder.Services.AddAuthorization(options =>
		{
			options.ConfigureAuthorizationPolicies();
		});

		builder.Services.ConfigureAuthorizationHandlers();

		var connectionString = configuration["DefaultConnection"];
		if (connectionString == null)
		{
			throw new ArgumentNullException(nameof(connectionString), "DefaultConnection is null in environment variables. Please configure it.");
		}

		builder.ConfigureDatabaseContext(configuration);

		builder.Services.AddAuthentication();

		// Controllers and JSON configuration
		builder.Services.ConfigureControllers();
		builder.Services.ConfigureServices();
		builder.Services.ConfigureJwtAuthentication(builder.Configuration);
		builder.Services.AddEndpointsApiExplorer();

		// currently disabled
		// builder.Services.ConfigureRateLimiter();
	}
}
