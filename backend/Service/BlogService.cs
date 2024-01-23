using Contracts.Dto;
using Domain.Models;
using FluentValidation;
using Infrastructure.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Service;

public class BlogService
{
	private readonly DatabaseContext context;
	private readonly IValidator<Blog> validator;
	private readonly ILogger<BlogService> logger;
	private readonly GetAuthenticatedUserIdService getAuthenticatedUserIdService;

	public BlogService(DatabaseContext dbContext, IValidator<Blog> validator, GetAuthenticatedUserIdService getAuthenticatedUserIdService, ILogger<BlogService> logger)
	{
		context = dbContext;
		this.validator = validator;
		this.getAuthenticatedUserIdService = getAuthenticatedUserIdService;
		this.logger = logger;
	}

	public async Task<ActionResult<BlogDTO>> GetBlogsAsync()
	{
		try
		{
			var blogs = await context.Blogs
				.OrderByDescending(e => e.CreatedAt)
				.Include(blog => blog.User)
				.Select(blog => new
				{
					blog.Id,
					blog.Description,
					blog.Author,
					blog.Text,
					blog.CreatedAt,
					blog.UserId,
					User = new
					{
						blog.User.Username
					}
				})
				.ToListAsync();

			return new BlogDTO
			{
				Blogs = blogs,
			};
		}
		catch (Exception ex)
		{
			logger.LogError($"GetBlogsAsync: An error occurred. Error: {ex.Message}");
			throw;
		}
	}

	public async Task<ActionResult<SingleBlogDTO>> GetBlogAsync(int id)
	{
		try
		{
			var blog = await context.Blogs
				.Where(b => b.Id == id)
				.Include(blog => blog.User)
				.Select(blog => new
				{
					blog.Id,
					blog.Description,
					blog.Author,
					blog.Text,
					blog.CreatedAt,
					blog.UserId,
					User = new
					{
						blog.User.Username
					}
				})
				.SingleOrDefaultAsync();

			if (blog == null) return new NotFoundResult();

			return new SingleBlogDTO
			{
				Blog = new[] { blog },
			};
		}
		catch (Exception ex)
		{
			logger.LogError($"GetBlogAsync: An error occurred. Error: {ex.Message}");
			throw;
		}
	}

	public async Task<IActionResult> UpdateBlogAsync(int id, Blog blog, ControllerBase controller)
	{
		try
		{
			if (id != blog.Id) return new BadRequestResult();

			if (!BlogExists(id))
			{
				return new NotFoundResult();
			}

			// Get authenticated user id
			var userId = getAuthenticatedUserIdService.GetUserId(controller.User);
			var isAuthorized = await context.Blogs.AnyAsync(b => b.Id == id && b.UserId == userId);

			if (!isAuthorized)
			{
				return new UnauthorizedResult();
			}

			// Update the blog
			context.Entry(blog).State = EntityState.Modified;
			await context.SaveChangesAsync();

			return new NoContentResult();
		}
		catch (DbUpdateConcurrencyException ex)
		{
			logger.LogError($"UpdateBlogAsync: Concurrency conflict occurred. Error: {ex.Message}");
			if (!BlogExists(id))
				return new NotFoundResult();
			throw;
		}
		catch (Exception ex)
		{
			logger.LogError($"UpdateBlogAsync: An error occurred. Error: {ex.Message}");
			throw;
		}
	}

	public async Task<ActionResult<Blog>> CreateBlogAsync(Blog blog, ControllerBase controller)
	{
		try
		{
			var validationResult = await validator.ValidateAsync(blog);

			if (!validationResult.IsValid)
			{
				return new BadRequestObjectResult(validationResult.Errors);
			}

			var userId = getAuthenticatedUserIdService.GetUserId(controller.User);

			if (userId == null)
			{
				return new UnauthorizedResult();
			}

			blog.UserId = (int)userId;

			blog.User = await context.Users.FindAsync(blog.UserId);

			context.Blogs.Add(blog);
			await context.SaveChangesAsync();

			var responseBlog = new
			{
				blog.Id,
				blog.Description,
				blog.Author,
				blog.Text,
				blog.CreatedAt,
				blog.UserId,
				User = new
				{
					blog.User.Username,
				}
			};

			return new CreatedAtActionResult("GetBlog", "Blog", new { id = blog.Id }, responseBlog);
		}
		catch (Exception ex)
		{
			logger.LogError($"CreateBlogAsync: An error occurred. Error: {ex.Message}");
			throw;
		}
	}

	public async Task<IActionResult> DeleteBlogAsync(int id)
	{
		try
		{
			var blog = await context.Blogs.FindAsync(id);
			if (blog == null)
			{
				logger.LogWarning($"DeleteBlogAsync: Blog with ID {id} not found.");
				return new NotFoundResult();
			}

			context.Blogs.Remove(blog);
			await context.SaveChangesAsync();

			logger.LogInformation($"DeleteBlogAsync: Blog with ID {id} successfully deleted.");

			return new NoContentResult();
		}
		catch (DbUpdateConcurrencyException ex)
		{
			logger.LogError($"DeleteBlogAsync: Concurrency conflict occurred. Error: {ex.Message}");
		}
		catch (Exception ex)
		{
			logger.LogError($"DeleteBlogAsync: An error occurred. Error: {ex.Message}");
			throw;
		}

		return new NoContentResult();
	}

	private bool BlogExists(int id)
	{
		return context.Blogs.Any(e => e.Id == id);
	}
}
