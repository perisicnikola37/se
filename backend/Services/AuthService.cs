using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Vega.Classes;
using Vega.Interfaces;
using Vega.Models;

public class AuthService : IAuthService
{
	private readonly MainDatabaseContext _context;
	private readonly IConfiguration _configuration;

	public AuthService(MainDatabaseContext context, IConfiguration configuration)
	{
		_context = context;
		_configuration = configuration;
	}

	private string GenerateJwtToken(User user)
	{
		var issuer = _configuration["Jwt:Issuer"];
		var audience = _configuration["Jwt:Audience"];
		var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

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
			SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
		};

		var tokenHandler = new JwtSecurityTokenHandler();
		return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
	}

	public async Task<LoggedInUser> LogInUserAsync(LogInUser user)
	{
		var authenticatedUser = await _context.Users
			.FirstOrDefaultAsync(u => u.Email == user.Email);

		if (authenticatedUser == null || !VerifyPassword(user.Password, authenticatedUser.Password))
		{
			return null;
		}

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
		if (await _context.Users.AnyAsync(u => u.Email == userRegistration.Email))
		{
			return new ConflictObjectResult(new { message = "Email is already registered" });
		}

		var newUser = new User
		{
			Username = userRegistration.Username,
			Email = userRegistration.Email,
			AccountType = userRegistration.AccountType,
		};

		var hashedPassword = HashPassword(userRegistration.Password);
		newUser.Password = hashedPassword;

		_context.Users.Add(newUser);
		await _context.SaveChangesAsync();

		var jwtToken = GenerateJwtToken(newUser);

		return newUser;
	}

	private string HashPassword(string password)
	{
		using (var sha256 = SHA256.Create())
		{
			byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

			return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
		}
	}

	private bool VerifyPassword(string inputPassword, string hashedPassword)
	{
		using (var sha256 = SHA256.Create())
		{
			byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(inputPassword));
			var inputHashedPassword = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

			return string.Equals(inputHashedPassword, hashedPassword, StringComparison.OrdinalIgnoreCase);
		}
	}
}
