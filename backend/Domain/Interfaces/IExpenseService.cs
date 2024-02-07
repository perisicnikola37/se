using Contracts.Dto;
using Contracts.Filter;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Domain.Interfaces;

public interface IExpenseService
{
	Task<PagedResponseDto<List<ExpenseResponseDto>>> GetExpensesAsync(PaginationFilterDto filter);
	Task<ActionResult<IEnumerable<Expense>>> GetLatestExpensesAsync();
	Task<ActionResult<Expense>> GetExpenseAsync(int id);
	Task<ActionResult<Expense>> CreateExpenseAsync(Expense expense);
	Task<IActionResult> UpdateExpenseAsync(int id, Expense updatedExpense);
	Task<IActionResult> DeleteExpenseByIdAsync(int id);
	Task<ActionResult<int>> GetTotalAmountOfExpensesAsync();
	Task<IActionResult> DeleteAllExpensesAsync();
}