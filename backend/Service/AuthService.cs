using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Contracts.Dto;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Service;

public class AuthService(IDatabaseContext context, IConfiguration configuration) : IAuthService
{
	public async Task<UserDto?> LogInUserAsync(LogInUser user)
	{
		var authenticatedUser = await context.Users
			.FirstOrDefaultAsync(u => u.Email == user.Email);
		
		// TODO why return null, are there other approches
		if (authenticatedUser == null || !VerifyPassword(user.Password, authenticatedUser.Password)) return null;

		var jwtToken = GenerateJwtToken(authenticatedUser);

		var userWithToken = new UserDto
		{
			Id = authenticatedUser.Id,
			Username = authenticatedUser.Username,
			Email = authenticatedUser.Email,
			AccountType = authenticatedUser.AccountType,
			Token = jwtToken
		};

		return userWithToken;
	}
	
	 public async Task<ActionResult<UserDto>> RegisterUserAsync(User userRegistration)
    {
        if (await context.Users.AnyAsync(u => u.Email == userRegistration.Email))
            return new ConflictObjectResult(new { message = "Email is already registered" });

        var newUser = new User
        {
            Username = userRegistration.Username,
            Email = userRegistration.Email,
            AccountType = userRegistration.AccountType
        };

        var token = GenerateJwtToken(newUser);

        var hashedPassword = HashPassword(userRegistration.Password);
        newUser.Password = hashedPassword;

        context.Users.Add(newUser);
        await context.SaveChangesAsync();

        var userDto = new UserDto
        {
            Id = newUser.Id,
            Username = newUser.Username,
            Email = newUser.Email,
            AccountType = newUser.AccountType,
            CreatedAt = newUser.CreatedAt,
            Token = token
        };

        return userDto;
    }

	private string GenerateJwtToken(User user)
	{
		var issuer = configuration["Jwt:Issuer"];
		var audience = configuration["Jwt:Audience"];
		var key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"] ??
										  "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJkZGFzYWRoYXNiZCBhc2RhZHMgc2Rhc3AgZGFzIGRhc2RhcyBhc2RhcyBkYXNkIGFzZGFzZGFzZCBhcyBkYXNhZGFzIGFzIGRhcyBkYXNhZGFzIGFzIGRhcyBkYXNhZGFzZGFzZCBhcyBkYXNhIGRhcyBkYXNhIGRhcyBkYXNhIGRhcyBkYXNhIGRhcyBkYXNhIGRhcyBkYXNhIGFzIGRhcyBkYXNhIGRhcyBkYXNhZGFzIGRhcyBkYXNhZGphcyBkYXNhIGRhcyBkYXNhIGRhcyBkYXNhIGRhcyBkYXNhIGRhcyBkYXNhIGRhcyBkYXNhIGRhcyBkYXNhIGRhcyBkYXNhIGRhcyBkYXNhIGRhcyBkYXNhZGphcyIsImlhdCI6MTYzNDEwNTUyMn0.S7G4f8pW7sGJ7t9PIShNElA0RRve-HlPfZRvX8hnZ6c");

		var tokenDescriptor = new SecurityTokenDescriptor
		{
			Subject = new ClaimsIdentity(new[]
			{
				new Claim("Id", user.Id.ToString()),
				new Claim(JwtRegisteredClaimNames.Sub, user.Username),
				new Claim(JwtRegisteredClaimNames.Email, user.Email),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
			}),
			Expires = DateTime.UtcNow.AddSeconds(10),
			Issuer = issuer,
			Audience = audience,
			SigningCredentials =
				new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
		};

		var tokenHandler = new JwtSecurityTokenHandler();
		return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
	}

	public static string HashPassword(string password)
	{
		var hashedBytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));

		return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
	}

	public static bool VerifyPassword(string inputPassword, string hashedPassword)
	{
		var hashedBytes = SHA256.HashData(Encoding.UTF8.GetBytes(inputPassword));
		var inputHashedPassword = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

		return string.Equals(inputHashedPassword, hashedPassword, StringComparison.OrdinalIgnoreCase);
	}
}