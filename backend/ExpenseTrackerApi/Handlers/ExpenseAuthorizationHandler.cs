using Infrastructure.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerApi.Handlers;

public class ExpenseAuthorizationRequirement : IAuthorizationRequirement;

public class ExpenseAuthorizationHandler(IHttpContextAccessor httpContextAccessor, DatabaseContext dbContext)
    : AuthorizationHandler<ExpenseAuthorizationRequirement>
{
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        ExpenseAuthorizationRequirement requirement)
    {
        // TODO refactoring technique, if you extract 
        var userIdClaim = context.User.Claims.FirstOrDefault(c => c.Type == "Id");

        if (userIdClaim != null)
        {
            Console.WriteLine($"Claim Type: {userIdClaim.Type}, Claim Value: {userIdClaim.Value}");

            if (int.TryParse(userIdClaim.Value, out var userId))
            {
                var expenseId = GetIdFromUrl();

                var expense = await dbContext.Expenses.FirstOrDefaultAsync(b => b.Id == expenseId);

                if (expense != null && expense.UserId == userId)
                {
                    Console.WriteLine("Entering the condition");

                    // Detach the existing tracked entity if any
                    dbContext.Entry(expense).State = EntityState.Detached;

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
        // Todo returning -1 is a red flag. it still returns int, throw an exception
        var id = httpContextAccessor.HttpContext?.Request.RouteValues["id"];
        if (id != null && int.TryParse(id.ToString(), out var result)) return result;

        return -1;
    }
}