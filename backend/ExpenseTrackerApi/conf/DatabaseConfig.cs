namespace ExpenseTrackerApi.conf;

using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

public static class DatabaseConfig
{
	// To use Docker, set useDocker environment variable to true in appsettings.json
	public static void ConfigureDatabaseContext(this WebApplicationBuilder builder, IConfiguration configuration)
	{
		bool useDocker = configuration["useDocker"] != null && bool.TryParse(configuration["useDocker"], out var parsedValue) && parsedValue;

		var configBuilder = new ConfigurationBuilder()
			.SetBasePath(AppContext.BaseDirectory)
			.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
			.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

		if (useDocker)
		{
			configBuilder.AddJsonFile("appsettings.Docker.json", optional: false, reloadOnChange: true);
		}

		var connectionString = configBuilder.Build()["DefaultConnection"];
		if (connectionString == null) throw new ArgumentNullException(nameof(connectionString), "DefaultConnection is null in environment variables. Please configure it.");

		builder.Services.AddDbContext<DatabaseContext>(options =>
		{
			options.UseMySql(
				connectionString,
				new MySqlServerVersion(new Version(8, 0, 35)),
				b => b.MigrationsAssembly("ExpenseTrackerApi")
			);
		});
	}
}
