using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Seeders
{
	public class IncomeGroupSeeder : ISeeder
	{
		private readonly string[] incomeGroupNames =
		[
			"Januarski prihodi",
			"Februarski prihodi",
			"Martovski prihodi",
			"Aprilski prihodi",
			"Majski prihodi",
			"Junski prihodi",
			"Julski prihodi",
			"Avgustovski prihodi",
			"Septembarski prihodi",
			"Oktobarski prihodi",
			"Novembarski prihodi",
			"Decembarski prihodi"
		];

		private readonly string[] incomeGroupDescriptions =
		[
			"Ovo su prihodi koji su ostvareni u januaru.",
			"Ovo su prihodi koji su ostvareni u februaru.",
			"Ovo su prihodi koji su ostvareni u martu.",
			"Ovo su prihodi koji su ostvareni u aprilu.",
			"Ovo su prihodi koji su ostvareni u maju.",
			"Ovo su prihodi koji su ostvareni u junu.",
			"Ovo su prihodi koji su ostvareni u julu.",
			"Ovo su prihodi koji su ostvareni u avgustu.",
			"Ovo su prihodi koji su ostvareni u septembru.",
			"Ovo su prihodi koji su ostvareni u oktobru.",
			"Ovo su prihodi koji su ostvareni u novembru.",
			"Ovo su prihodi koji su ostvareni u decembru."
		];

		public void Seed(ModelBuilder modelBuilder)
		{
			Random random = new();

			for (int i = 1; i <= 10; i++)
			{
				int randomIndex = random.Next(0, incomeGroupNames.Length);

				modelBuilder.Entity<IncomeGroup>().HasData(new IncomeGroup
				{
					Id = i,
					Name = incomeGroupNames[randomIndex],
					Description = incomeGroupDescriptions[randomIndex],
					CreatedAt = DateTime.Now.AddDays(-i),
					UserId = 1
				});
			}
		}
	}
}
