using Domain.Models;
using FluentValidation;
using Infrastructure.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
<<<<<<< f761a5018843e15eab521a84eef4beaa05940ca2
public class BlogController(MainDatabaseContext context, GetAuthenticatedUserIdService getAuthenticatedUserIdService, IValidator<Blog> validator)
=======
public class BlogController(MainDatabaseContext context, GetAuthenticatedUserIdService getAuthenticatedUserIdService)
>>>>>>> cbe03f3e9b25aa91bdb2cd899c243733b4f0e7d6
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
<<<<<<< f761a5018843e15eab521a84eef4beaa05940ca2
	public async Task<IActionResult> PutBlog(int id, Blog blog)
	{
		if (id != blog.Id) return BadRequest();
=======
    public async Task<IActionResult> PutBlog(int id, Blog blog)
    {
        if (id != blog.Id) return BadRequest();
>>>>>>> cbe03f3e9b25aa91bdb2cd899c243733b4f0e7d6

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
<<<<<<< f761a5018843e15eab521a84eef4beaa05940ca2
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

=======
		var userId = getAuthenticatedUserIdService.GetUserId(User);
		blog.UserId = (int)userId!;

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

>>>>>>> cbe03f3e9b25aa91bdb2cd899c243733b4f0e7d6
	private bool BlogExists(int id)
	{
		return context.Blogs.Any(e => e.Id == id);
	}
}