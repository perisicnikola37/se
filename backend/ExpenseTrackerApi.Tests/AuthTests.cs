using Domain.Interfaces;
using FluentValidation;
using Moq;

namespace ExpenseTrackerApi.Tests;

public class AuthControllerTests
{
	private readonly Mock<DatabaseContext> dbContextMock = new();
	private readonly Mock<IAuthService> authServiceMock = new();
	private readonly Mock<IValidator<User>> validatorMock = new();

	[Fact]
	public async Task LogInUser_ReturnsOkResult()
	{
		// Arrange
		// Create a mock user object for login with email and password
		var user = new LogInUser { Email = "nikola@e-invoices.online", Password = "06032004" };

		// Create a mock user object with a token for the expected result
		var userWithToken = new LoggedInUser
		{
			Username = "nikola",
			Email = "nikola@e-invoices.online",
			Token = "someToken"
		};

		// Set up the mock AuthService to return the mock user with a token when LogInUserAsync is called
		authServiceMock.Setup(x => x.LogInUserAsync(user)).ReturnsAsync(userWithToken);

		var authController = new AuthController(dbContextMock.Object, authServiceMock.Object, validatorMock.Object);

		// Act
		var result = await authController.LogInUser(user);

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
		authServiceMock.Setup(x => x.LogInUserAsync(user)).ReturnsAsync((LoggedInUser)null!);

		var authController = new AuthController(dbContextMock.Object, authServiceMock.Object, validatorMock.Object);

		// Act
		var result = await authController.LogInUser(user);

		// Assert
		var actionResult = Assert.IsType<ActionResult<User>>(result);
		var notFoundResult = Assert.IsType<NotFoundObjectResult>(actionResult.Result);

		var actualErrorMessage =
			(string)notFoundResult.Value.GetType().GetProperty("message")!.GetValue(notFoundResult.Value, null)!;

		Assert.Equal("Invalid email or password", actualErrorMessage);
	}
}