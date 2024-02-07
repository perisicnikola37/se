using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Seeders;

public class BlogSeeder : ISeeder
{
	public void Seed(ModelBuilder modelBuilder)
	{
		for (int i = 1; i <= 100; i++)
		{
			modelBuilder.Entity<Blog>().HasData(new Blog
			{
				Id = i,
				Description = $"Blog Description {i}",
				Author = $"http://author{i}.com",
				Text = $"Blog Text {i}",
				UserId = 1
			});
		}
	}
}
