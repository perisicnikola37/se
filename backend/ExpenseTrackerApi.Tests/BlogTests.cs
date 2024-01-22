using System;
using System.Threading.Tasks;
using Domain.Models;
using Infrastructure.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presentation.Controllers;
using Service;
using Xunit;

public class BlogControllerTests
{
	[Fact]
	public async Task PostBlog_ReturnsCreatedAtAction()
	{
		var options = new DbContextOptionsBuilder<MainDatabaseContext>()
			.UseInMemoryDatabase(databaseName: "TestDatabase")
			.Options;

		using var context = new MainDatabaseContext(options);
		var getAuthenticatedUserIdService = new GetAuthenticatedUserIdService();

		var controller = new BlogController(context, getAuthenticatedUserIdService);

		var blog = new Blog
		{
			Description = "Some valid description",
			Author = "Mark Zuckerberg",
			Text = "Some valid text",
			UserId = 1,
			CreatedAt = DateTime.Now
		};

		// Act
		var result = await controller.PostBlog(blog);

		// Assert
		var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
		var createdBlog = Assert.IsType<Blog>(createdAtActionResult.Value);

		Assert.Equal(blog.Description, createdBlog.Description);
		Assert.Equal(blog.Author, createdBlog.Author);
		Assert.Equal(blog.Text, createdBlog.Text);
		Assert.Equal(blog.UserId, createdBlog.UserId);
		Assert.Equal(blog.CreatedAt, createdBlog.CreatedAt);
	}
}
