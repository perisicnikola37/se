using Infrastructure.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerApi.Handlers;

public class IncomeAuthorizationRequirement : IAuthorizationRequirement;

public class IncomeAuthorizationHandler(IHttpContextAccessor httpContextAccessor, DatabaseContext dbContext)
    : AuthorizationHandler<IncomeAuthorizationRequirement>
{
    private readonly DatabaseContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

    private readonly IHttpContextAccessor _httpContextAccessor =
        httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        IncomeAuthorizationRequirement requirement)
    {
        var userIdClaim = context.User.Claims.FirstOrDefault(c => c.Type == "Id");

        if (userIdClaim != null)
        {
            Console.WriteLine($"Claim Type: {userIdClaim.Type}, Claim Value: {userIdClaim.Value}");

            if (int.TryParse(userIdClaim.Value, out var userId))
            {
                var incomeId = GetIdFromUrl();

                var income = await _dbContext.Incomes.FirstOrDefaultAsync(b => b.Id == incomeId);

                if (income != null && income.UserId == userId)
                {
                    Console.WriteLine("Entering the condition");

                    // Detach the existing tracked entity if any
                    _dbContext.Entry(income).State = EntityState.Detached;

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