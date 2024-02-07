using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Service;

public class SummaryService(IDatabaseContext context, GetAuthenticatedUserIdService getAuthenticatedUserId)
    : ISummaryService
{
    public async Task<object> GetLast7DaysIncomesAndExpensesAsync(ControllerBase controller)
    {
        var authenticatedUserId = getAuthenticatedUserId.GetUserId(controller.User);

        var startOfLast7Days = DateTime.UtcNow.AddDays(-6); // for the last 7 days

        var allExpenses = await context.Expenses
            .ToListAsync();

        var allIncomes = await context.Incomes
            .Where(i => i.UserId == authenticatedUserId && i.CreatedAt.Date >= startOfLast7Days.Date)
            .ToListAsync();

        var dateList = Enumerable.Range(0, 7)
            .Select(offset => startOfLast7Days.AddDays(offset))
            .Select(date => date.ToString("dd.MM"))
            .Reverse()
            .ToList();

        var last7DaysExpenses = allExpenses
            .Where(e => e.CreatedAt.Date >= startOfLast7Days.Date)
            .GroupBy(e => e.CreatedAt.Date.ToString("dd.MM"))
            .ToDictionary(group => group.Key, group => group.Sum(e => e.Amount));

        var last7DaysIncomes = allIncomes
            .GroupBy(i => i.CreatedAt.Date.ToString("dd.MM"))
            .ToDictionary(group => group.Key, group => group.Sum(i => i.Amount));

        // Sort dictionaries based on dateList order
        last7DaysExpenses = dateList.ToDictionary(date => date, date => last7DaysExpenses.GetValueOrDefault(date, 0));
        last7DaysIncomes = dateList.ToDictionary(date => date, date => last7DaysIncomes.GetValueOrDefault(date, 0));

        var response = new
        {
            expenses = last7DaysExpenses,
            incomes = last7DaysIncomes
        };

        return response;
    }
}