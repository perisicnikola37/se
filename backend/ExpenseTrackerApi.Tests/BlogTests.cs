using System;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Presentation.Controllers;  
using Infrastructure.Contexts;

public class BlogControllerTests
{
	[Fact]
	public async Task PostBlog_ValidBlog_ReturnsCreatedAtAction()
	{
		var controller = new BlogController(new MainDatabaseContext()); context class
		var blog = new Blog
		{
			Description = "Valid Description",
			Author = "John Doe",
			Text = "Valid Text",
			UserId = 1, 
			Created_at = DateTime.Now
		};

		var result = await controller.PostBlog(blog);

		var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
		var returnedBlog = Assert.IsType<Blog>(createdAtActionResult.Value);

		Assert.Equal(blog.Description, returnedBlog.Description);
		Assert.Equal(blog.Author, returnedBlog.Author);
		Assert.Equal(blog.Text, returnedBlog.Text);
		Assert.Equal(blog.UserId, returnedBlog.UserId);
	}
}
