using Contracts.Dto.Blogs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Domain.Interfaces;

public interface IBlogService
{
	Task<IEnumerable<object>> GetBlogsAsync(ControllerBase controller);
	Task<ActionResult<object>> GetBlogAsync(int id);
	Task<ActionResult<Blog>> CreateBlogAsync(Blog blog, ControllerBase controller);
	Task<IActionResult> UpdateBlogAsync(int id, Blog blog, ControllerBase controller);
	Task<IActionResult> DeleteBlogAsync(int id);
}