using System.Security.Cryptography;
using System.Text;
using Domain.Exceptions;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts
{
    public static class SeedData
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            try
            {
                SeedUsers(modelBuilder);
                // SeedBlogs(modelBuilder);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred during seeding: {ex.Message}");
                throw new OnModelCreatingException("SeedData.cs");
            }
        }

        private static void SeedUsers(ModelBuilder modelBuilder)
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


        // private static void SeedBlogs(ModelBuilder modelBuilder)
        // {
        //     for (int i = 1; i <= 100; i++)
        //     {
        //         modelBuilder.Entity<Blog>().HasData(new Blog
        //         {
        //             Id = i,
        //             Description = $"Blog Description {i}",
        //             Author = $"http://author{i}.com",
        //             Text = $"Blog Text {i}"
        //         });
        //     }
        // }

        private static string HashPassword(string password)
        {
            var hashedBytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));

            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
    }
}
