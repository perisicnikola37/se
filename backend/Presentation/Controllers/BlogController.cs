using Contracts.Dto;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Presentation.Controllers;

[Route("api/blogs")]
[ApiController]
[EnableRateLimiting("fixed")]
public class BlogController(IBlogService blogService) : ControllerBase
{
	// GET: api/Blog
	[HttpGet]
	public async Task<ActionResult<IEnumerable<BlogDTO>>> GetBlogsAsync()
	{
		return Ok(await blogService.GetBlogsAsync());
	}

	// GET: api/Blog/5
	[HttpGet("{id}")]
	public async Task<ActionResult<SingleBlogDTO>> GetBlogAsync(int id)
	{
		return await blogService.GetBlogAsync(id);
	}

	// PUT: api/Blog/5
	[HttpPut("{id}")]
	[Authorize("BlogOwnerPolicy")]
	public async Task<IActionResult> PutBlogAsync(int id, Blog blog)
	{
		return await blogService.UpdateBlogAsync(id, blog, this);
	}

	// POST: api/Blog
	[HttpPost]
	public async Task<ActionResult<Blog>> PostBlogAsync(Blog blog)
	{

		return await blogService.CreateBlogAsync(blog, this);
	}

	// DELETE: api/Blog/5
	[HttpDelete("{id}")]
	[Authorize("BlogOwnerPolicy")]
	public async Task<IActionResult> DeleteBlogAsync(int id)
	{
		return await blogService.DeleteBlogAsync(id);
	}
}