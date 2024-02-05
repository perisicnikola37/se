using Contracts.Dto.Blogs;
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
	public async Task<ActionResult<IEnumerable<BlogDto>>> GetBlogsAsync()
	{
		return Ok(await blogService.GetBlogsAsync(this));
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<object>> GetBlogAsync(int id)
	{
		return await blogService.GetBlogAsync(id);
	}

	[HttpPut("{id}")]
	[Authorize("BlogOwnerPolicy")]
	public async Task<IActionResult> PutBlogAsync(int id, Blog blog)
	{
		return await blogService.UpdateBlogAsync(id, blog, this);
	}

	[HttpPost]
	public async Task<ActionResult<Blog>> PostBlogAsync(Blog blog)
	{
		return await blogService.CreateBlogAsync(blog, this);
	}

	[HttpDelete("{id}")]
	[Authorize("BlogOwnerPolicy")]
	public async Task<IActionResult> DeleteBlogAsync(int id)
	{
		return await blogService.DeleteBlogAsync(id);
	}
}