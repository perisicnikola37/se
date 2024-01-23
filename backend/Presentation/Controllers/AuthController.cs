using Domain.Interfaces;
using Domain.Models;
using FluentValidation;
using Infrastructure.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
	private readonly IAuthService _authService;
	private readonly MainDatabaseContext _context;
	private readonly IValidator<User> _validator;

	public AuthController(MainDatabaseContext context, IAuthService authService, IValidator<User> validator)
	{
		_context = context;
		_authService = authService;
		_validator = validator;
	}

	[HttpPost("Login")]
	public async Task<ActionResult<User>> LogInUser(LogInUser user)
	{
		// AuthService
		var userWithToken = await _authService.LogInUserAsync(user);

		if (userWithToken == null!) return NotFound(new { message = "Invalid email or password" });

		return Ok(new { message = "User logged in successfully", user = userWithToken });
	}

	// POST: api/Auth/Register
	[HttpPost("Register")]
	public async Task<ActionResult<User>> RegisterUser(User userRegistration)
	{
		var validationResult = await _validator.ValidateAsync(userRegistration);
		if (!validationResult.IsValid)
		{
			return BadRequest(validationResult.Errors);
		}
		else
		{
			return await _authService.RegisterUserAsync(userRegistration);
		}
	}

	/// POST: api/Auth/CurrentUser
	[HttpGet("CurrentUser")]
	public ActionResult<LoggedInUser> GetCurrentUser()
	{
		foreach (var claim in User.Claims) Console.WriteLine($"Claim Type: {claim.Type}, Claim Value: {claim.Value}");

		var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "Id");
		if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var userId))
			return BadRequest(new { message = "Invalid user claims" });

		var user = _context.Users
			.Include(u => u.Expenses)
			.Include(u => u.Incomes)
			.Include(u => u.Blogs)
			.FirstOrDefault(u => u.Id == userId);

		if (user == null) return NotFound(new { message = "User not found" });

		return Ok(user);
	}
}