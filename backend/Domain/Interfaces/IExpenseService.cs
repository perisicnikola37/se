using Contracts.Dto;
using Contracts.Filter;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Service
{
    public interface IExpenseService
    {
        Task<PagedResponse<List<ExpenseResponse>>> GetExpensesAsync(PaginationFilter filter, ControllerBase controller);
        Task<List<Expense>> GetLatestExpensesAsync();
        Task<Response<Expense>?> GetExpenseAsync(int id);
        Task<ActionResult<Expense>> CreateExpenseAsync(Expense expense, ControllerBase controller);
        Task<IActionResult> UpdateExpenseAsync(int id, Expense updatedExpense, ControllerBase controller);
        Task<IActionResult> DeleteExpenseByIdAsync(int id);
        Task<ActionResult<int>> GetTotalAmountOfExpensesAsync();
    }
}
