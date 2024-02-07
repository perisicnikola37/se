using Contracts.Dto;
using Domain.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;

namespace ExpenseTrackerApi.Tests;

public class AuthControllerTests
{
    private readonly Mock<IAuthService> authServiceMock = new();
    private readonly Mock<DatabaseContext> dbContextMock = new();
    private readonly Mock<IHttpContextAccessor> httpContextAccessor = new();
    private readonly Mock<ILogger<AuthController>> logger = new();
    private readonly Mock<IValidator<User>> validatorMock = new();

    [Fact]
    public async Task LogInUser_ReturnsOkResult()
    {
        // Arrange
        // Create a mock user object for login with email and password
        var user = new LogInUser { Email = "nikola@e-invoices.online", Password = "06032004" };

        // Create a mock user object with a token for the expected result
        var userWithToken = new UserDto
        {
            Username = "nikola",
            Email = "nikola@e-invoices.online",
            Token = "someToken"
        };

        // Set up the mock AuthService to return the mock user with a token when LogInUserAsync is called
        authServiceMock.Setup(x => x.LogInUserAsync(user)).ReturnsAsync(userWithToken);

        var authController = new AuthController(authServiceMock.Object, validatorMock.Object, logger.Object,
            new GetCurrentUserService(httpContextAccessor.Object, dbContextMock.Object));

        // Act
        var result = await authController.LogInUserAsync(user);

        // Assert
        Assert.IsType<ActionResult<User>>(result);
    }

    [Fact]
    public async Task LogInUser_ReturnsNotFound()
    {
        // Arrange
        // Create a mock user object for login with invalid email address
        var user = new LogInUser { Email = "nonexistent@e-invoices.online", Password = "password123" };

        // Set up the mock AuthService to return null when LogInUserAsync is called for a nonexistent user
        authServiceMock.Setup(x => x.LogInUserAsync(user)).ReturnsAsync((UserDto)null!);

        var authController = new AuthController(authServiceMock.Object, validatorMock.Object, logger.Object,
            new GetCurrentUserService(httpContextAccessor.Object, dbContextMock.Object));

        // Act
        var result = await authController.LogInUserAsync(user);

        // Assert
        var actionResult = Assert.IsType<ActionResult<User>>(result);
        var notFoundResult = Assert.IsType<UnauthorizedObjectResult>(actionResult.Result);

        var actualErrorMessage =
            (string)notFoundResult.Value?.GetType().GetProperty("message")!.GetValue(notFoundResult.Value, null)!;

        Assert.Equal("Invalid email or password", actualErrorMessage);
    }
}