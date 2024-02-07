using System.Security.Cryptography;
using System.Text;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Seeders;

public class UserSeeder : ISeeder
{
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
	}

	private static string HashPassword(string password)
	{
		var hashedBytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));

		return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
	}
}
