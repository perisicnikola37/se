using Contracts.Dto.Blogs;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using FluentValidation;
using Infrastructure.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Service;

public class BlogService(
    DatabaseContext context,
    IValidator<Blog> validator,
    IGetAuthenticatedUserIdService getAuthenticatedUserId,
    ILogger<BlogService> logger) : IBlogService
{
    public async Task<ActionResult<SingleBlogDto>> GetBlogAsync(int id)
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
                        blog.User!.Username
                    }
                })
                .SingleOrDefaultAsync();

            if (blog == null) return new NotFoundResult();

            return new SingleBlogDto
            {
                Blog = new[] { blog }
            };
        }
        catch (Exception ex)
        {
            logger.LogError($"GetBlogAsync: An error occurred. Error: {ex.Message}");
            throw;
        }
    }

    public async Task<ActionResult<Blog>> CreateBlogAsync(Blog blog, ControllerBase controller)
    {
        try
        {
            var validationResult = await validator.ValidateAsync(blog);

            if (!validationResult.IsValid) return new BadRequestObjectResult(validationResult.Errors);

            var userId = getAuthenticatedUserId.GetUserId(controller.User);

            if (userId == null) return new UnauthorizedResult();

            blog.UserId = (int)userId;

            var user = await context.Users.FindAsync(blog.UserId);

            if (user == null)
            {
                return new NotFoundResult();
            }

            blog.User = user;

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
                    blog.User!.Username
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

    public async Task<IActionResult> UpdateBlogAsync(int id, Blog blog, ControllerBase controller)
    {
        try
        {
            if (id != blog.Id) return new BadRequestResult();

            if (!BlogExists(id)) return new NotFoundResult();

            var authenticatedUserId = getAuthenticatedUserId.GetUserId(controller.User);

            // Check if authenticatedUserId has a value
            if (authenticatedUserId.HasValue)
            {
                // attach authenticated user id
                blog.UserId = authenticatedUserId.Value;

                context.Entry(blog).State = EntityState.Modified;

                try
                {
                    await context.SaveChangesAsync();
                }
                catch (Exception)
                {
                    if (!BlogExists(id)) return new NotFoundResult();
                    throw new ConflictException("BlogService.cs");
                }

                return new NoContentResult();
            }

            return new BadRequestResult();
        }
        catch (Exception ex)
        {
            logger.LogError($"UpdateBlogAsync: An error occurred. Error: {ex.Message}");
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
        catch (ConflictException ex)
        {
            logger.LogError($"DeleteBlogAsync: Concurrency conflict occurred. Error: {ex.Message}");
            throw new ConflictException("BlogService.cs");
        }
        catch (Exception ex)
        {
            logger.LogError($"DeleteBlogAsync: An error occurred. Error: {ex.Message}");
            throw;
        }
    }

    public async Task<ActionResult<BlogDto>> GetBlogsAsync()
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
                        blog.User!.Username
                    }
                })
                .ToListAsync();

            return new BlogDto
            {
                Blogs = blogs
            };
        }
        catch (Exception ex)
        {
            logger.LogError($"GetBlogsAsync: An error occurred. Error: {ex.Message}");
            throw;
        }
    }

    private bool BlogExists(int id)
    {
        return context.Blogs.Any(e => e.Id == id);
    }
}