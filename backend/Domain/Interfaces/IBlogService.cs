using Contracts.Dto;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Domain.Interfaces
{
    public interface IBlogService
    {
        Task<ActionResult<BlogDTO>> GetBlogsAsync();
        Task<ActionResult<SingleBlogDTO>> GetBlogAsync(int id);
        Task<ActionResult<Blog>> CreateBlogAsync(Blog blog, ControllerBase controller);
        Task<IActionResult> UpdateBlogAsync(int id, Blog blog, ControllerBase controller);
        Task<IActionResult> DeleteBlogAsync(int id);
    }
}
