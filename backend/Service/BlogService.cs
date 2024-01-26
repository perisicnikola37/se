using Contracts.Dto;
using Domain.Exceptions;
using Domain.Models;
using FluentValidation;
using Infrastructure.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Service;

public class BlogService(DatabaseContext _context, IValidator<Blog> _validator, GetAuthenticatedUserIdService _getAuthenticatedUserIdService, ILogger<BlogService> _logger)
{
	private readonly DatabaseContext _context = _context;
	private readonly IValidator<Blog> _validator = _validator;
	private readonly ILogger<BlogService> _logger = _logger;
	private readonly GetAuthenticatedUserIdService _getAuthenticatedUserIdService = _getAuthenticatedUserIdService;

	public async Task<ActionResult<BlogDTO>> GetBlogsAsync()
	{
		try
		{
			var blogs = await _context.Blogs
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
			_logger.LogError($"GetBlogsAsync: An error occurred. Error: {ex.Message}");
			throw;
		}
	}

	public async Task<ActionResult<SingleBlogDTO>> GetBlogAsync(int id)
	{
		try
		{
			var blog = await _context.Blogs
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
			_logger.LogError($"GetBlogAsync: An error occurred. Error: {ex.Message}");
			throw;
		}
	}

	public async Task<ActionResult<Blog>> CreateBlogAsync(Blog blog, ControllerBase controller)
	{
		try
		{
			var validationResult = await _validator.ValidateAsync(blog);

			if (!validationResult.IsValid) return new BadRequestObjectResult(validationResult.Errors);

			var userId = _getAuthenticatedUserIdService.GetUserId(controller.User);

			if (userId == null) return new UnauthorizedResult();

			blog.UserId = (int)userId;

			blog.User = await _context.Users.FindAsync(blog.UserId);

			_context.Blogs.Add(blog);
			await _context.SaveChangesAsync();

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
			_logger.LogError($"CreateBlogAsync: An error occurred. Error: {ex.Message}");
			throw;
		}
	}

	public async Task<IActionResult> UpdateBlogAsync(int id, Blog blog, ControllerBase controller)
	{
		try
		{
			if (id != blog.Id) return new BadRequestResult();

			if (!BlogExists(id)) return new NotFoundResult();

			int? authenticatedUserId = _getAuthenticatedUserIdService.GetUserId(controller.User);

			// Check if authenticatedUserId has a value
			if (authenticatedUserId.HasValue)
			{
				// attach authenticated user id
				blog.UserId = authenticatedUserId.Value;

				_context.Entry(blog).State = EntityState.Modified;

				try
				{
					await _context.SaveChangesAsync();
				}
				catch (Exception)
				{
					if (!BlogExists(id)) return new NotFoundResult();
					throw new ConflictException("BlogService.cs");
				}

				return new NoContentResult();
			}
			else
			{
				return new BadRequestResult();
			}
		}
		catch (Exception ex)
		{
			_logger.LogError($"UpdateBlogAsync: An error occurred. Error: {ex.Message}");
			throw;
		}
	}
	
	public async Task<IActionResult> DeleteBlogAsync(int id)
	{
		try
		{
			var blog = await _context.Blogs.FindAsync(id);
			if (blog == null)
			{
				_logger.LogWarning($"DeleteBlogAsync: Blog with ID {id} not found.");
				return new NotFoundResult();
			}

			_context.Blogs.Remove(blog);
			await _context.SaveChangesAsync();

			_logger.LogInformation($"DeleteBlogAsync: Blog with ID {id} successfully deleted.");

			return new NoContentResult();
		}
		catch (ConflictException ex)
		{
			_logger.LogError($"DeleteBlogAsync: Concurrency conflict occurred. Error: {ex.Message}");
			throw new ConflictException("BlogService.cs");
		}
		catch (Exception ex)
		{
			_logger.LogError($"DeleteBlogAsync: An error occurred. Error: {ex.Message}");
			throw;
		}
	}

	private bool BlogExists(int id)
	{
		return _context.Blogs.Any(e => e.Id == id);
	}
}
