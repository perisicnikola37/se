using Contracts.Dto;
using Contracts.Filter;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Domain.Interfaces;

public interface IExpenseService
{
	Task<PagedResponseDto<List<ExpenseResponseDto>>> GetExpensesAsync(PaginationFilterDto filter,
		IHttpContextAccessor httpContextAccessor);
	Task<object> GetLatestExpensesAsync(ControllerBase controller);
	Task<ActionResult<Expense>> GetExpenseAsync(int id);
	Task<ActionResult<Expense>> CreateExpenseAsync(Expense expense, ControllerBase controller);
	Task<IActionResult> UpdateExpenseAsync(int id, Expense updatedExpense, ControllerBase controller);
	Task<IActionResult> DeleteExpenseByIdAsync(int id);
	Task<ActionResult<int>> GetTotalAmountOfExpensesAsync();
	Task<IActionResult> DeleteAllExpensesAsync(ControllerBase controller);
}