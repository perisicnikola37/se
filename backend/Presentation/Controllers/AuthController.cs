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
    GetCurrentUserService getCurrentUserService) : ControllerBase
{
    private readonly IAuthService authService = authService ?? throw new ArgumentNullException(nameof(authService));

    private readonly GetCurrentUserService getCurrentUserService =
        getCurrentUserService ?? throw new ArgumentNullException(nameof(getCurrentUserService));

    private readonly ILogger<AuthController> logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IValidator<User> validator = validator ?? throw new ArgumentNullException(nameof(validator));

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

            return StatusCode(500, "An error occurred during login");
        }
    }

    [HttpPost("register")]
    public async Task<ActionResult<User>> RegisterUserAsync(User userRegistration)
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

            return StatusCode(500, new { message = "An error occurred during user registration" });
        }
    }

    [HttpGet("user")]
    public ActionResult<LoggedInUser> GetCurrentUser()
    {
        try
        {
            return getCurrentUserService.GetCurrentUser();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while getting the current user.");

            return StatusCode(500, new { message = "An error occurred while getting the current user" });
        }
    }
}