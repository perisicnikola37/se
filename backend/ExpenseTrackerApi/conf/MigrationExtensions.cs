namespace ExpenseTrackerApi.conf;

using Infrastructure.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public static class MigrationExtensions
{
	public static void ApplyDatabaseMigrations(this IApplicationBuilder app)
	{
		using var scope = app.ApplicationServices.CreateScope();
		var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
		db.Database.Migrate();
	}
}
