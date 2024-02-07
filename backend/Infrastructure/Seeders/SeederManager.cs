using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Seeders;

public class SeederManager
{
	private readonly List<ISeeder> seeders;

	public SeederManager()
	{
		seeders =
		[
			new UserSeeder(),
			// new BlogSeeder(),
		];
	}

	public void SeedAll(ModelBuilder modelBuilder)
	{
		foreach (var seeder in seeders)
		{
			seeder.Seed(modelBuilder);
		}
	}
}
