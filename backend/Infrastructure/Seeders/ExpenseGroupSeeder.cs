using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Seeders
{
	public class ExpenseGroupSeeder : ISeeder
	{
		private readonly string[] expenseGroupNames =
		[
			"Troskovi za januar",
			"Troskovi za februar",
			"Troskovi za mart",
			"Troskovi za april",
			"Troskovi za maj",
			"Troskovi za jun",
			"Troskovi za jul",
			"Troskovi za avgust",
			"Troskovi za septembar",
			"Troskovi za oktobar",
			"Troskovi za novembar",
			"Troskovi za decembar"
		];

		private readonly string[] expeseGroupDescriptions =
		[
			"Ovo su troskovi koji su ostvareni u januaru.",
			"Ovo su troskovi koji su ostvareni u februaru.",
			"Ovo su troskovi koji su ostvareni u martu.",
			"Ovo su troskovi koji su ostvareni u aprilu.",
			"Ovo su troskovi koji su ostvareni u maju.",
			"Ovo su troskovi koji su ostvareni u junu.",
			"Ovo su troskovi koji su ostvareni u julu.",
			"Ovo su troskovi koji su ostvareni u avgustu.",
			"Ovo su troskovi koji su ostvareni u septembru.",
			"Ovo su troskovi koji su ostvareni u oktobru.",
			"Ovo su troskovi koji su ostvareni u novembru.",
			"Ovo su troskovi koji su ostvareni u decembru."
		];

		public void Seed(ModelBuilder modelBuilder)
		{
			Random random = new();

			for (int i = 1; i <= 10; i++)
			{
				int randomIndex = random.Next(0, expenseGroupNames.Length);

				modelBuilder.Entity<ExpenseGroup>().HasData(new ExpenseGroup
				{
					Id = i,
					Name = expenseGroupNames[randomIndex],
					Description = expeseGroupDescriptions[randomIndex],
					CreatedAt = DateTime.Now.AddDays(-i),
					UserId = 1
				});
			}
		}
	}
}
