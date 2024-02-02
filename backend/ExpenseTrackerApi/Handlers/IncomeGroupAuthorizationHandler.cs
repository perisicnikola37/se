using Infrastructure.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerApi.Handlers
{
	public class IncomeGroupAuthorizationRequirement : IAuthorizationRequirement;

	public class IncomeGroupAuthorizationHandler(IHttpContextAccessor httpContextAccessor, DatabaseContext dbContext) : AuthorizationHandler<IncomeGroupAuthorizationRequirement>
	{
		private readonly DatabaseContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

		private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));

		protected override async Task HandleRequirementAsync(
	AuthorizationHandlerContext context,
	IncomeGroupAuthorizationRequirement requirement)
		{
			var userIdClaim = context.User.Claims.FirstOrDefault(c => c.Type == "Id");

			if (userIdClaim != null && int.TryParse(userIdClaim.Value, out var userId))
			{
				var incomeGroupId = GetIdFromUrl();

				var incomeGroup = await _dbContext.IncomeGroups.FirstOrDefaultAsync(b => b.Id == incomeGroupId && b.UserId == userId);

				if (incomeGroup != null)
				{
					Console.WriteLine("Entering the condition");
					context.Succeed(requirement);
				}
				else
				{
					Console.WriteLine("Condition not met");
				}
			}
			else
			{
				Console.WriteLine("Invalid user ID claim or user ID claim not found");
			}
		}


		private int GetIdFromUrl()
		{
			var id = _httpContextAccessor.HttpContext?.Request.RouteValues["id"];
			if (id != null && int.TryParse(id.ToString(), out var result)) return result;

			return -1;
		}
	}
}
