using Domain.Interfaces;
using Moq;

namespace ExpenseTrackerApi.Tests;

public class AuthControllerTests
{
    [Fact]
    public async Task LogInUser_ReturnsOkResult()
    {
        var dbContextMock = new Mock<MainDatabaseContext>();
        var authServiceMock = new Mock<IAuthService>();

        var user = new LogInUser { Email = "nikola@e-invoices.online", Password = "06032004" };
        var userWithToken = new LoggedInUser
            { Username = "nikola", Email = "nikola@e-invoices.online", Token = "someToken" };

        authServiceMock.Setup(x => x.LogInUserAsync(user)).ReturnsAsync(userWithToken);

        var authController = new AuthController(dbContextMock.Object, authServiceMock.Object);

        var result = await authController.LogInUser(user);

        Assert.IsType<ActionResult<User>>(result);
    }

    [Fact]
    public async Task LogInUser_ReturnsNotFound()
    {
        var dbContextMock = new Mock<MainDatabaseContext>();
        var authServiceMock = new Mock<IAuthService>();

        var user = new LogInUser { Email = "nonexistent@e-invoices.online", Password = "password123" };

        authServiceMock.Setup(x => x.LogInUserAsync(user)).ReturnsAsync((LoggedInUser)null!);

        var authController = new AuthController(dbContextMock.Object, authServiceMock.Object);

        var result = await authController.LogInUser(user);

        var actionResult = Assert.IsType<ActionResult<User>>(result);
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(actionResult.Result);

        var actualErrorMessage =
            (string)notFoundResult.Value.GetType().GetProperty("message")!.GetValue(notFoundResult.Value, null)!;
        Assert.Equal("Invalid email or password", actualErrorMessage);
    }
}