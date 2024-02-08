namespace ExpenseTrackerApi.conf;

using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

public static class DatabaseConfig
{
	public static void ConfigureDatabaseContext(this WebApplicationBuilder builder, IConfiguration configuration)
	{
		bool useDocker = configuration["useDocker"] != null && bool.TryParse(configuration["useDocker"], out var parsedValue) && parsedValue;

		if (useDocker)
		{
			builder.Services.AddDbContext<DatabaseContext>(options =>
			{
				options.UseMySql(
					"server=mysql;user=root;port=3306;Connect Timeout=5;database=first_database;password=password",
					new MySqlServerVersion(new Version(8, 0, 35)),
					b => b.MigrationsAssembly("ExpenseTrackerApi")
				);
			});
		}
		else
		{
			var connectionString = configuration["DefaultConnection"];
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
}
