using ExpenseTrackerApi.Tests.Fixtures;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;

namespace ExpenseTrackerApi.Tests;

public class BlogControllerTests(DatabaseFixture fixture) : IClassFixture<DatabaseFixture>
{
    private readonly Mock<GetAuthenticatedUserIdService> getAuthenticated = new();
    private readonly Mock<ILogger<BlogService>> loggerMock = new();
    private readonly Mock<IValidator<Blog>> validatorMock = new();

    [Fact]
    public async Task GetBlogsAsync_ReturnsOkResult()
    {
        // Arrange
        var context = fixture.Context;

        // Add some test data to the in-memory database
        await context.Blogs.AddRangeAsync(new List<Blog>
        {
            new()
            {
                Description = "Blog 1", Author = "Author 1", Text = "Text 1", UserId = 1, CreatedAt = DateTime.Now
            },
            new() { Description = "Blog 2", Author = "Author 2", Text = "Text 2", UserId = 2, CreatedAt = DateTime.Now }
        });
        await context.SaveChangesAsync();

        // Create the service and controller
        var blogService = new BlogService(context, validatorMock.Object, getAuthenticated.Object,
            loggerMock.Object);
        var controller = new BlogController(blogService);

        // Act
        var result = await controller.GetBlogsAsync();

        // Assert
        Assert.IsType<OkObjectResult>(result.Result);
    }
}