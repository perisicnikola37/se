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
	[HttpGet]
	public async Task<ActionResult<IEnumerable<Blog>>> GetBlogsAsync()
	{
		return Ok(await blogService.GetBlogsAsync());
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<Blog>> GetBlogAsync(int id)
	{
		return await blogService.GetBlogAsync(id);
	}

	[HttpPut("{id}")]
	[Authorize("BlogOwnerPolicy")]
	public async Task<IActionResult> PutBlogAsync(int id, Blog blog)
	{
		return await blogService.UpdateBlogAsync(id, blog);
	}

	[HttpPost]
	public async Task<ActionResult<Blog>> PostBlogAsync(Blog blog)
	{
		return await blogService.CreateBlogAsync(blog);
	}

	[HttpDelete("{id}")]
	[Authorize("BlogOwnerPolicy")]
	public async Task<IActionResult> DeleteBlogAsync(int id)
	{
		return await blogService.DeleteBlogAsync(id);
	}
}