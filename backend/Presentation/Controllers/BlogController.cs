using Contracts.Dto;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlogController(BlogService _blogService) : ControllerBase
{
	// GET: api/Blog
	[HttpGet]
	public async Task<ActionResult<IEnumerable<BlogDTO>>> GetBlogs()
	{
		return Ok(await _blogService.GetBlogsAsync());
	}

	// GET: api/Blog/5
	[HttpGet("{id}")]
	public async Task<ActionResult<SingleBlogDTO>> GetBlog(int id)
	{
		return await _blogService.GetBlogAsync(id);
	}

	// PUT: api/Blog/5
	[HttpPut("{id}")]
	public async Task<IActionResult> PutBlog(int id, Blog blog)
	{
		return await _blogService.UpdateBlogAsync(id, blog, this);
	}

	// POST: api/Blog
	[HttpPost]
	public async Task<ActionResult<Blog>> PostBlog(Blog blog)
	{

		return await _blogService.CreateBlogAsync(blog, this);
	}

	// DELETE: api/Blog/5
	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteBlog(int id)
	{
		return await _blogService.DeleteBlogAsync(id);
	}
}