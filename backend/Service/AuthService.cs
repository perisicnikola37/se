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

public class AuthService(IDatabaseContext context, IConfiguration configuration, IEmailService emailService) : IAuthService
{
	public async Task<UserDto?> LogInUserAsync(LogInUser user)
	{
		var authenticatedUser = await context.Users
			.FirstOrDefaultAsync(u => u.Email == user.Email);

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

		var hashedPassword = HashPassword(userRegistration.Password);
		newUser.Password = hashedPassword;

		context.Users.Add(newUser);
		await context.SaveChangesAsync();

		var claims = new List<Claim>
		{
			new Claim("Id", newUser.Id.ToString()),
			new Claim(JwtRegisteredClaimNames.Sub, newUser.Username),
			new Claim(JwtRegisteredClaimNames.Email, newUser.Email),
			new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
		};

		var token = GenerateJwtToken(newUser);

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
			Expires = DateTime.Now.AddHours(1),
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

	private static string GenerateResetToken()
	{
		var tokenLength = 32;
		var randomBytes = new byte[tokenLength];

		using (var rng = new RNGCryptoServiceProvider())
		{
			rng.GetBytes(randomBytes);
		}

		// Use URL-safe base64 encoding
		var base64Token = Convert.ToBase64String(randomBytes);
		var urlSafeToken = base64Token.Replace('+', '-').Replace('/', '_');

		return urlSafeToken;
	}

	public async Task<bool> ForgotPasswordAsync(string userEmail)
	{
		var user = await context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);

		if (user == null)
			return false;

		var resetToken = GenerateResetToken();
		user.ResetToken = resetToken;
		user.ResetTokenExpiration = DateTime.UtcNow.AddHours(1);

		await context.SaveChangesAsync();

		var resetLink = $"{configuration["AppUrl"]}/reset-password?token={resetToken}&email={userEmail}";

		string emailBody = $@"<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">
        <html dir=""ltr"" lang=""en"">
          <head>
            <meta content=""text/html; charset=UTF-8"" http-equiv=""Content-Type"" />
            <link href=""https://cdn.jsdelivr.net/npm/tailwindcss@2.2.19/dist/tailwind.min.css"" rel=""stylesheet"">
          </head>
          <div style=""display:none;overflow:hidden;line-height:1px;opacity:0;max-height:0;max-width:0"">Reset password<div></div>
          </div>
        
          <body style=""background-color:#ffffff;font-family:'Inter', 'ui-sans-serif', 'system-ui', '-apple-system', 'Segoe UI', Roboto, 'Helvetica Neue', Arial, 'Noto Sans', sans-serif"">
            <div class='max-w-2xl mx-auto p-4'>
                <img src='https://i.postimg.cc/VsKQJpRb/logo.png' alt='Linear Logo' class='block mx-auto mb-4' style='width: 42px; height: 42px;'>
                <h1 class='text-2xl font-semibold text-gray-800 mb-6'>Reset password</h1>
                <p class='text-gray-700 text-sm mt-6'>
   				You have requested to reset your password. Click the following link to reset it:
				<br/>
    			<a href='{resetLink}' class='text-blue-500 hover:underline'>Click here</a>
				</p>
                <hr class='border-t border-gray-300 my-8'>
                <p class='text-gray-700 text-sm'>ExpenseTracker&trade;</p>
            </div>
          </body>
        </html>";

		var subject = "Reset password - Expense Tracker&trade;";
		await emailService.SendEmail(new EmailRequestDto { ToEmail = userEmail }, subject, emailBody);

		return true;
	}

	public async Task<bool> ResetPasswordAsync(string userEmail, string token, string newPassword)
	{
		var user = await context.Users.FirstOrDefaultAsync(u => u.Email == userEmail && u.ResetToken == token && u.ResetTokenExpiration > DateTime.UtcNow);

		if (user == null)
			return false;

		user.Password = HashPassword(newPassword);
		user.ResetToken = null;
		user.ResetTokenExpiration = null;

		await context.SaveChangesAsync();

		return true;
	}
}