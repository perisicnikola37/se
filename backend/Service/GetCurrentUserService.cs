using Contracts.Dto;
using Domain.Interfaces;
using Infrastructure.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Service;

public class GetCurrentUserService(IHttpContextAccessor httpContextAccessor, DatabaseContext context)
    : IGetCurrentUserService
{
    private const string UserIdClaimType = "Id";

    public ActionResult<UserDto> GetCurrentUser()
    {
        foreach (var claim in httpContextAccessor.HttpContext.User.Claims)
            Console.WriteLine($"Claim Type: {claim.Type}, Claim Value: {claim.Value}");

        var userIdClaim = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == UserIdClaimType);

        if (userIdClaim == null) return new BadRequestObjectResult(new { message = "User ID claim is not present" });

        if (!int.TryParse(userIdClaim.Value, out var userId))
            return new BadRequestObjectResult(new { message = "Invalid user ID claim value" });

        var user = context.Users
            .Include(u => u.Expenses)
            .Include(u => u.Incomes)
            .Include(u => u.Blogs)
            .FirstOrDefault(u => u.Id == userId);

        if (user == null) return new NotFoundObjectResult(new { message = "User not found" });

        return new OkObjectResult(user);
    }
}