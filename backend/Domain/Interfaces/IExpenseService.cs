using Contracts.Dto;
using Contracts.Filter;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Domain.Interfaces;

public interface IExpenseService
{
    Task<PagedResponseDto<List<ExpenseResponse>>> GetExpensesAsync(PaginationFilterDto filter,
        ControllerBase controller);

    Task<List<Expense>> GetLatestExpensesAsync(ControllerBase controller);
    Task<ActionResult<Expense>> GetExpenseAsync(int id);
    Task<ActionResult<Expense>> CreateExpenseAsync(Expense expense, ControllerBase controller);
    Task<IActionResult> UpdateExpenseAsync(int id, Expense updatedExpense, ControllerBase controller);
    Task<IActionResult> DeleteExpenseByIdAsync(int id);
    Task<ActionResult<int>> GetTotalAmountOfExpensesAsync();
}