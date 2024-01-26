using Contracts.Dto.Blogs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Domain.Interfaces;

public interface IBlogService
{
    Task<ActionResult<BlogDto>> GetBlogsAsync();
    Task<ActionResult<SingleBlogDto>> GetBlogAsync(int id);
    Task<ActionResult<Blog>> CreateBlogAsync(Blog blog, ControllerBase controller);
    Task<IActionResult> UpdateBlogAsync(int id, Blog blog, ControllerBase controller);
    Task<IActionResult> DeleteBlogAsync(int id);
}