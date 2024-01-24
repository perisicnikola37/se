using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Contexts; 
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Service;

public class GetCurrentUserService(IHttpContextAccessor httpContextAccessor, DatabaseContext context)
{
	private const string UserIdClaimType = "Id"; 
	private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
	private readonly DatabaseContext _context = context ?? throw new ArgumentNullException(nameof(context));

	public ActionResult<LoggedInUser> GetCurrentUser()
	{
		foreach (var claim in _httpContextAccessor.HttpContext.User.Claims)
		{
			Console.WriteLine($"Claim Type: {claim.Type}, Claim Value: {claim.Value}");
		}

		var userIdClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == UserIdClaimType);

		if (userIdClaim == null)
		{
			return new BadRequestObjectResult(new { message = "User ID claim is not present" });
		}

		if (!int.TryParse(userIdClaim.Value, out var userId))
		{
			return new BadRequestObjectResult(new { message = "Invalid user ID claim value" });
		}

		var user = _context.Users
			.Include(u => u.Expenses)
			.Include(u => u.Incomes)
			.Include(u => u.Blogs)
			.FirstOrDefault(u => u.Id == userId);

		if (user == null)
		{
			return new NotFoundObjectResult(new { message = "User not found" });
		}

		return new OkObjectResult(user);
	}
}
