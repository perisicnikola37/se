using Bogus;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Seeders
{
	public class BlogSeeder : ISeeder
	{
		private readonly Faker<Blog> _blogFaker;

		public BlogSeeder()
		{
			_blogFaker = new Faker<Blog>()
				.RuleFor(b => b.Description, f => f.Lorem.Sentence())
				.RuleFor(b => b.Author, f => $"http://author{f.IndexVariable}.com")
				.RuleFor(b => b.Text, f => f.Lorem.Paragraphs(3))
				.RuleFor(b => b.UserId, 1);
		}

		public void Seed(ModelBuilder modelBuilder)
		{
			for (int i = 1; i <= 100; i++)
			{
				var blog = _blogFaker.Generate();
				blog.Id = i;

				modelBuilder.Entity<Blog>().HasData(blog);
			}
		}
	}
}
