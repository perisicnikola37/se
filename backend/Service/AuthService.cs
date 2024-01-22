using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Service;

public class AuthService(IMainDatabaseContext context, IConfiguration configuration) : IAuthService
{
    public async Task<LoggedInUser?> LogInUserAsync(LogInUser user)
    {
        var authenticatedUser = await context.Users
            .FirstOrDefaultAsync(u => u.Email == user.Email);

        if (authenticatedUser == null || !VerifyPassword(user.Password, authenticatedUser.Password)) return null;

        var jwtToken = GenerateJwtToken(authenticatedUser);

        var userWithToken = new LoggedInUser
        {
            Id = authenticatedUser.Id,
            Username = authenticatedUser.Username,
            Email = authenticatedUser.Email,
            AccountType = authenticatedUser.AccountType,
            Token = jwtToken
        };

        return userWithToken;
    }

    public async Task<ActionResult<User>> RegisterUserAsync(User userRegistration)
    {
        if (await context.Users.AnyAsync(u => u.Email == userRegistration.Email))
            return new ConflictObjectResult(new { message = "Email is already registered" });

        var newUser = new User
        {
            Username = userRegistration.Username,
            Email = userRegistration.Email,
            AccountType = userRegistration.AccountType
        };

        var hashedPassword = HashPassword(userRegistration.Password);
        newUser.Password = hashedPassword;

        context.Users.Add(newUser);
        await context.SaveChangesAsync();
        GenerateJwtToken(newUser);

        return newUser;
    }

    private string GenerateJwtToken(User user)
    {
        var issuer = configuration["Jwt:Issuer"];
        var audience = configuration["Jwt:Audience"];
        var key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"] ??
                                          "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJkZGFzYWRoYXNiZCBhc2RhZHMgc2Rhc3AgZGFzIGRhc2RhcyBhc2RhcyBkYXNkIGFzZGFzZGFzZCBhcyBkYXNhZGFzIGFzIGRhcyBkYXNhZGFzIGFzIGRhcyBkYXNhZGFzZGFzZCBhcyBkYXNhIGRhcyBkYXNhIGRhcyBkYXNhIGRhcyBkYXNhIGRhcyBkYXNhIGRhcyBkYXNhIGFzIGRhcyBkYXNhIGRhcyBkYXNhZGFzIGRhcyBkYXNhZGphcyBkYXNhIGRhcyBkYXNhIGRhcyBkYXNhIGRhcyBkYXNhIGRhcyBkYXNhIGRhcyBkYXNhIGRhcyBkYXNhIGRhcyBkYXNhIGRhcyBkYXNhIGRhcyBkYXNhZGFzIGRhcyBkYXNhIGRhcyBkYXNhIGRhcyBkYXNhIGRhcyBkYXNhIGRhcyBkYXNhIGRhcyBkYXNhIGRhcyBkYXNhZGphcyIsImlhdCI6MTYzNDEwNTUyMn0.S7G4f8pW7sGJ7t9PIShNElA0RRve-HlPfZRvX8hnZ6c");

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("Id", user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }),
            Expires = DateTime.UtcNow.AddMinutes(5),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
    }

    public string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
    }

    private bool VerifyPassword(string inputPassword, string hashedPassword)
    {
        using (var sha256 = SHA256.Create())
        {
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(inputPassword));
            var inputHashedPassword = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

            return string.Equals(inputHashedPassword, hashedPassword, StringComparison.OrdinalIgnoreCase);
        }
    }
}