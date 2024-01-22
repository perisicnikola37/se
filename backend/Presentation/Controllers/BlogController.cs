using Domain.Models;
using FluentValidation;
using Infrastructure.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlogController(MainDatabaseContext context, GetAuthenticatedUserIdService getAuthenticatedUserIdService, IValidator<Blog> validator)
	: ControllerBase
{
	// GET: api/Blog
	[HttpGet]
	public async Task<ActionResult<IEnumerable<Blog>>> GetBlogs()
	{
		var blogs = await context.Blogs.OrderByDescending(e => e.CreatedAt).ToListAsync();

		if (blogs.Count() != 0)
			return blogs;
		return NotFound();
	}

	// GET: api/Blog/5
	[HttpGet("{id}")]
	public async Task<ActionResult<Blog>> GetBlog(int id)
	{
		var blog = await context.Blogs.FindAsync(id);

		if (blog == null) return NotFound();

		return blog;
	}

	// PUT: api/Blog/5
	// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
	[HttpPut("{id}")]
	public async Task<IActionResult> PutBlog(int id, Blog blog)
	{
		if (id != blog.Id) return BadRequest();

		context.Entry(blog).State = EntityState.Modified;

		try
		{
			await context.SaveChangesAsync();
		}
		catch (DbUpdateConcurrencyException)
		{
			if (!BlogExists(id))
				return NotFound();
			throw;
		}

		return NoContent();
	}

	// POST: api/Blog
	// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
	[HttpPost]
	public async Task<ActionResult<Blog>> PostBlog(Blog blog)
	{
		var validationResult = await validator.ValidateAsync(blog);
		if (!validationResult.IsValid)
		{
			return BadRequest(validationResult.Errors);
		}

		var userId = getAuthenticatedUserIdService.GetUserId(User);
		if (userId == null)
		{
			return Unauthorized();
		}

		blog.UserId = (int)userId;

		context.Blogs.Add(blog);
		await context.SaveChangesAsync();

		return CreatedAtAction("GetBlog", new { id = blog.Id }, blog);
	}


	// DELETE: api/Blog/5
	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteBlog(int id)
	{
		var blog = await context.Blogs.FindAsync(id);
		if (blog == null) return NotFound();

		context.Blogs.Remove(blog);
		await context.SaveChangesAsync();

		return NoContent();
	}

	private bool BlogExists(int id)
	{
		return context.Blogs.Any(e => e.Id == id);
	}
}