using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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

    public async Task<LoggedInUser> LogInUserAsync(LogInUser user)
    {
        var authenticatedUser = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == user.Email && u.Password == user.Password);

        if (authenticatedUser == null)
        {
            return null;
        }

        var issuer = _configuration["Jwt:Issuer"];
        var audience = _configuration["Jwt:Audience"];
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("Id", authenticatedUser.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, authenticatedUser.Username),
                new Claim(JwtRegisteredClaimNames.Email, authenticatedUser.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }),
            Expires = DateTime.UtcNow.AddMinutes(5),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = tokenHandler.WriteToken(token);

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
}
