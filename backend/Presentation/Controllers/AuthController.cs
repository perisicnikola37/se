using Contracts.Dto;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
	public async Task<ActionResult<User>> LogInUserAsync(LogInUserDto user)
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

	[HttpPost("forgot/password")]
	public async Task<IActionResult> ForgotPasswordAsync([FromBody] ForgotPasswordRequestDto forgotPasswordRequest)
	{
		try
		{
			var isEmailSent = await authService.ForgotPasswordAsync(forgotPasswordRequest.UserEmail);

			if (isEmailSent)
			{
				logger.LogInformation("Password reset email sent successfully.");
				return Ok(new { message = "Password reset email sent successfully" });
			}

			logger.LogWarning("User not found for password reset.");
			return NotFound(new { message = "User not found for password reset" });
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "An error occurred during forgot password request.");
			throw new DatabaseException("AuthController.cs");
		}
	}

	[HttpPost("reset/password")]
	public async Task<IActionResult> ResetPasswordAsync([FromBody] ResetPasswordRequestDto resetPasswordRequest)
	{
		try
		{
			var isPasswordReset = await authService.ResetPasswordAsync(
				resetPasswordRequest.UserEmail,
				resetPasswordRequest.ResetToken,
				resetPasswordRequest.NewPassword);

			if (isPasswordReset)
			{
				logger.LogInformation("Password reset successfully.");
				return Ok(new { message = "Password reset successfully" });
			}

			logger.LogWarning("Invalid or expired token for password reset.");
			return BadRequest(new { message = "Invalid or expired token for password reset" });
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "An error occurred during password reset.");
			throw new DatabaseException("AuthController.cs");
		}
	}
}