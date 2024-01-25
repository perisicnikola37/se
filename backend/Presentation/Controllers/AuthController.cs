using Domain.Interfaces;
using Domain.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service;

namespace Presentation.Controllers
{
	[Route("api/auth")]
	[ApiController]
	public class AuthController(
		IAuthService authService,
		IValidator<User> validator,
		ILogger<AuthController> logger,
		GetCurrentUserService getCurrentUserService) : ControllerBase
	{
		private readonly IAuthService _authService = authService ?? throw new ArgumentNullException(nameof(authService));
		private readonly IValidator<User> _validator = validator ?? throw new ArgumentNullException(nameof(validator));
		private readonly GetCurrentUserService _getCurrentUserService = getCurrentUserService ?? throw new ArgumentNullException(nameof(getCurrentUserService));
		private readonly ILogger<AuthController> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

		[HttpPost("login")]
		public async Task<ActionResult<User>> LogInUserAsync(LogInUser user)
		{
			try
			{
				var userWithToken = await _authService.LogInUserAsync(user);

				if (userWithToken == null)
				{
					_logger.LogWarning("Invalid email or password for login attempt.");

					// Return a 403 Forbidden status code
					return Unauthorized(new { message = "Invalid email or password" });
				}

				_logger.LogInformation("User logged in successfully.");

				return Ok(new { message = "User logged in successfully", user = userWithToken });
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred during login.");

				return StatusCode(500, new { message = "An error occurred during login" });
			}
		}

		[HttpPost("register")]
		public async Task<ActionResult<User>> RegisterUserAsync(User userRegistration)
		{
			try
			{
				var validationResult = await _validator.ValidateAsync(userRegistration);

				if (!validationResult.IsValid)
				{
					_logger.LogWarning("User registration validation failed.");

					return BadRequest(validationResult.Errors);
				}
				else
				{
					var registeredUser = await _authService.RegisterUserAsync(userRegistration);
					_logger.LogInformation("User registered successfully.");

					return registeredUser;
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred during user registration.");

				return StatusCode(500, new { message = "An error occurred during user registration" });
			}
		}

		[HttpGet("user")]
		public ActionResult<LoggedInUser> GetCurrentUser()
		{
			try
			{
				return _getCurrentUserService.GetCurrentUser();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred while getting the current user.");

				return StatusCode(500, new { message = "An error occurred while getting the current user" });
			}
		}
	}
}
