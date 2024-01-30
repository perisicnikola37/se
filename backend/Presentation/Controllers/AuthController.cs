using Contracts.Dto;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service;

namespace Presentation.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController(
	IAuthService authService,
	IValidator<User> validator,
	ILogger<AuthController> logger,
	IGetCurrentUserService getCurrentUserService) : ControllerBase
{
	[HttpPost("login")]
	public async Task<ActionResult<User>> LogInUserAsync(LogInUser user)
	{
		try
		{
			var userWithToken = await authService.LogInUserAsync(user);

			if (userWithToken == null)
			{
				logger.LogWarning("Invalid email or password for login attempt.");

				// Return a 403 Forbidden status code
				return Unauthorized(new { message = "Invalid email or password" });
			}

			logger.LogInformation("User logged in successfully.");
			
			return Ok(new { message = "User logged in successfully", user = userWithToken });
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "An error occurred during login.");
			//TODO database exception? custom exceptions for business logic
			throw new DatabaseException("AuthController.cs");
		}
	}

	[HttpPost("register")]
	public async Task<ActionResult<UserDto>> RegisterUserAsync(User userRegistration)
	{
		try
		{
			var validationResult = await validator.ValidateAsync(userRegistration);

			if (!validationResult.IsValid)
			{
				logger.LogWarning("User registration validation failed.");

				return BadRequest(validationResult.Errors);
			}

			var registeredUser = await authService.RegisterUserAsync(userRegistration);
			logger.LogInformation("User registered successfully.");

			return registeredUser;
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "An error occurred during user registration.");
			// TODO same wrong exception
			// TODO Exception bubbling mechanism in .NET 
			throw new DatabaseException("AuthController.cs");
		}
	}

	[HttpGet("user")]
	public ActionResult<UserDto> GetCurrentUser()
	{
		try
		{
			return getCurrentUserService.GetCurrentUser();
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "An error occurred while getting the current user.");

			throw new DatabaseException("AuthController.cs");
		}
	}
}