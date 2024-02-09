using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Seeders
{
	public class ExpenseSeeder : ISeeder
	{
		readonly Random random = new();
		private readonly string[] expenseDescriptions =
		[
			"Placanje racuna",
			"Kupovina garderobe",
			"Kupovina obuce",
			"Kupovina tehnike",
			"Kupovina namjestaja",
			"Kupovina knjiga",
			"Kupovina hrane"
		];

		public void Seed(ModelBuilder modelBuilder)
		{
			for (int i = 0; i < expenseDescriptions.Length && i < 10; i++)
			{
				modelBuilder.Entity<Expense>().HasData(new Expense
				{
					Id = i + 1,
					Description = expenseDescriptions[i],
					Amount = (float)(random.NextDouble() * 1000.0),
					CreatedAt = DateTime.Now.AddDays(-i),
					ExpenseGroupId = 1,
					UserId = 1
				});
			}
		}
	}
}
