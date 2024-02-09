using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Seeders
{
	public class IncomeSeeder : ISeeder
	{
		readonly Random random = new();
		private readonly string[] incomeDescriptions =
		[
			"Plata",
			"Freelance posao",
			"Povracaj investicije",
			"Povracaj investicije",
			"Povracaj investicije",
			"Povracaj investicije",
			"Povracaj investicije",
			"Povracaj investicije",
			"Povracaj investicije",
		];

		public void Seed(ModelBuilder modelBuilder)
		{
			for (int i = 0; i < incomeDescriptions.Length && i < 10; i++)
			{
				modelBuilder.Entity<Income>().HasData(new Income
				{
					Id = i + 1,
					Description = incomeDescriptions[i],
					Amount = (float)(random.NextDouble() * 1000.0),
					CreatedAt = DateTime.Now.AddDays(-i),
					IncomeGroupId = 1,
					UserId = 1
				});
			}
		}
	}
}
