using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Domain.Interfaces;

public interface IBlogService
{
	Task<IEnumerable<Blog>> GetBlogsAsync();
	Task<ActionResult<Blog>> GetBlogAsync(int id);
	Task<ActionResult<Blog>> CreateBlogAsync(Blog blog);
	Task<IActionResult> UpdateBlogAsync(int id, Blog blog);
	Task<IActionResult> DeleteBlogAsync(int id);
}