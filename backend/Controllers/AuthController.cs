using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Vega.Classes;
using Vega.Models;

namespace Vega.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly MainDatabaseContext _context;
    private readonly IConfiguration _configuration;

    public AuthController(MainDatabaseContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    /// POST: api/Auth/Login
    [HttpPost("Login")]
    public async Task<ActionResult<User>> LogInUser(LogInUser user)
    {
        var authenticatedUser = await _context.Users
           .FirstOrDefaultAsync(u => u.Email == user.Email && u.Password == user.Password);

        if (authenticatedUser == null)
        {
            return NotFound(new { message = "Invalid email or password" });
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

        return Ok(new { message = "User logged in successfully", user = userWithToken });
    }

    // POST: api/Auth/Register
    [HttpPost("Register")]
    public async Task<ActionResult<User>> RegisterUser(User userRegistration)
    {
        if (await _context.Users.AnyAsync(u => u.Email == userRegistration.Email))
        {
            return Conflict(new { message = "Email is already registered" });
        }

        var newUser = new User
        {
            Username = userRegistration.Username,
            Email = userRegistration.Email,
            AccountType = userRegistration.AccountType,
            Password = userRegistration.Password,
        };

        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();

        var issuer = _configuration["Jwt:Issuer"];
        var audience = _configuration["Jwt:Audience"];
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
            new Claim("Id", newUser.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, newUser.Username),
            new Claim(JwtRegisteredClaimNames.Email, newUser.Email),
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

        return CreatedAtAction("GetUser", new { id = newUser.Id }, new { message = "User registered successfully", user = newUser, token = jwtToken });
    }

    /// POST: api/Auth/CurrentUser
    [HttpGet("CurrentUser")]
    public ActionResult<LoggedInUser> GetCurrentUser()
    {
        foreach (var claim in User.Claims)
        {
            Console.WriteLine($"Claim Type: {claim.Type}, Claim Value: {claim.Value}");
        }

        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "Id");
        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
        {
            return BadRequest(new { message = "Invalid user claims" });
        }

        var user = _context.Users.Find(userId);

        if (user == null)
        {
            return NotFound(new { message = "User not found" });
        }

        return Ok(user);
    }
}