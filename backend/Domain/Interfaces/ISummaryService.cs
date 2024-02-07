using Microsoft.AspNetCore.Mvc;

namespace Domain.Interfaces;

public interface ISummaryService
{
    Task<object> GetLast7DaysIncomesAndExpensesAsync();
}