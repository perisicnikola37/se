using System.Security.Cryptography;
using System.Text;
using Bogus;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Seeders
{
	public class UserSeeder : ISeeder
	{
		private readonly Faker<User> _userFaker;

		public UserSeeder()
		{
			_userFaker = new Faker<User>()
				.RuleFor(u => u.Username, f => f.Internet.UserName())
				.RuleFor(u => u.Email, f => f.Internet.Email())
				.RuleFor(u => u.Password, f => HashPassword("password"))
				.RuleFor(u => u.AccountType, f => f.PickRandom("Administrator", "Regular"));
		}

		public void Seed(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>().HasData(new User
			{
				Id = 1,
				Username = "Administrator",
				Email = "admin@gmail.com",
				Password = HashPassword("password"),
				AccountType = "Administrator"
			});

			for (int i = 2; i <= 100; i++)
			{
				var user = _userFaker.Generate();
				user.Id = i;

				modelBuilder.Entity<User>().HasData(user);
			}
		}

		private static string HashPassword(string password)
		{
			var hashedBytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));

			return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
		}
	}
}
