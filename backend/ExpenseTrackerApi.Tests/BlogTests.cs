namespace ExpenseTrackerApi.Tests;

public class BlogControllerTests
{
	[Fact]
	public async Task GetBlogs_ReturnsListOfBlogs()
	{
		var options = new DbContextOptionsBuilder<MainDatabaseContext>()
			.UseInMemoryDatabase(databaseName: "TestDatabase")
			.Options;

		using var context = new MainDatabaseContext(options);
		var getAuthenticatedUserIdService = new GetAuthenticatedUserIdService();

		var controller = new BlogController(context, getAuthenticatedUserIdService);

		// Add some test blogs to the database
		await context.Blogs.AddRangeAsync(
			new Blog { Description = "Blog 1", Author = "John Doe", Text = "Text 1", UserId = 1, CreatedAt = DateTime.Now },
			new Blog { Description = "Blog 2", Author = "Jane Doe", Text = "Text 2", UserId = 1, CreatedAt = DateTime.Now }
		);
		await context.SaveChangesAsync();

		var result = await controller.GetBlogs();

		var blogs = Assert.IsType<List<Blog>>(result.Value);
		Assert.Equal(4, blogs.Count);
	}

	[Fact]
	public async Task GetBlog_ReturnsBlogById()
	{
		var options = new DbContextOptionsBuilder<MainDatabaseContext>()
			.UseInMemoryDatabase(databaseName: "TestDatabase")
			.Options;

		using var context = new MainDatabaseContext(options);
		var getAuthenticatedUserIdService = new GetAuthenticatedUserIdService();

		var controller = new BlogController(context, getAuthenticatedUserIdService);

		var testBlog = new Blog { Description = "Test Blog", Author = "John Doe", Text = "Test Text", UserId = 1, CreatedAt = DateTime.Now };
		await context.Blogs.AddAsync(testBlog);
		await context.SaveChangesAsync();

		var result = await controller.GetBlog(testBlog.Id);

		var blog = Assert.IsType<Blog>(result.Value);
		Assert.Equal(testBlog.Description, blog.Description);
		Assert.Equal(testBlog.Author, blog.Author);
		Assert.Equal(testBlog.Text, blog.Text);
		Assert.Equal(testBlog.UserId, blog.UserId);
		Assert.Equal(testBlog.CreatedAt, blog.CreatedAt);
	}

	[Fact]
	public async Task PutBlog_UpdatesBlog()
	{
		var options = new DbContextOptionsBuilder<MainDatabaseContext>()
			.UseInMemoryDatabase(databaseName: "TestDatabase")
			.Options;

		using var context = new MainDatabaseContext(options);
		var getAuthenticatedUserIdService = new GetAuthenticatedUserIdService();

		var controller = new BlogController(context, getAuthenticatedUserIdService);

		var testBlog = new Blog { Description = "Test Blog", Author = "John Doe", Text = "Test Text", UserId = 1, CreatedAt = DateTime.Now };
		await context.Blogs.AddAsync(testBlog);
		await context.SaveChangesAsync();

		testBlog.Description = "Updated Description";

		var result = await controller.PutBlog(testBlog.Id, testBlog);

		Assert.IsType<NoContentResult>(result);

		// Check if the blog was actually updated in the database
		var updatedBlog = await context.Blogs.FindAsync(testBlog.Id);
		Assert.Equal(testBlog.Description, updatedBlog.Description);
	}

	[Fact]
	public async Task DeleteBlog_RemovesBlog()
	{
		var options = new DbContextOptionsBuilder<MainDatabaseContext>()
			.UseInMemoryDatabase(databaseName: "TestDatabase")
			.Options;

		using var context = new MainDatabaseContext(options);
		var getAuthenticatedUserIdService = new GetAuthenticatedUserIdService();

		var controller = new BlogController(context, getAuthenticatedUserIdService);

		var testBlog = new Blog { Description = "Test Blog", Author = "John Doe", Text = "Test Text", UserId = 1, CreatedAt = DateTime.Now };
		await context.Blogs.AddAsync(testBlog);
		await context.SaveChangesAsync();

		// Act
		var result = await controller.DeleteBlog(testBlog.Id);

		// Assert
		Assert.IsType<NoContentResult>(result);

		var deletedBlog = await context.Blogs.FindAsync(testBlog.Id);
		Assert.Null(deletedBlog);
	}
}

