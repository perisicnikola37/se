using Infrastructure.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerApi.Handlers;

public class BlogAuthorizationRequirement : IAuthorizationRequirement;

public class BlogAuthorizationHandler(IHttpContextAccessor httpContextAccessor, DatabaseContext context)
    : AuthorizationHandler<BlogAuthorizationRequirement>
{
    private readonly DatabaseContext _context = context ?? throw new ArgumentNullException(nameof(context));

    private readonly IHttpContextAccessor _httpContextAccessor =
        httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        BlogAuthorizationRequirement requirement)
    {
        var userIdClaim = context.User.Claims.FirstOrDefault(c => c.Type == "Id");

        if (userIdClaim != null)
        {
            Console.WriteLine($"Claim Type: {userIdClaim.Type}, Claim Value: {userIdClaim.Value}");

            if (int.TryParse(userIdClaim.Value, out var userId))
            {
                var blogId = GetIdFromUrl();

                var blog = await _context.Blogs.FirstOrDefaultAsync(b => b.Id == blogId);

                if (blog != null && blog.UserId == userId)
                {
                    Console.WriteLine("Entering the condition");

                    // Detach the existing tracked entity if any
                    _context.Entry(blog).State = EntityState.Detached;

                    context.Succeed(requirement);
                }
                else
                {
                    Console.WriteLine("Condition not met");
                }
            }
            else
            {
                Console.WriteLine("Invalid user ID claim");
            }
        }
        else
        {
            Console.WriteLine("UserId claim not found");
        }
    }

    private int GetIdFromUrl()
    {
        var id = _httpContextAccessor.HttpContext?.Request.RouteValues["id"];
        if (id != null && int.TryParse(id.ToString(), out var result)) return result;

        return -1;
    }
}