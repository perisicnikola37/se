using Contracts.Dto;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Domain.Interfaces
{
	public interface IExpenseGroupService
	{
		Task<IEnumerable<ExpenseGroupDto>> GetExpenseGroupsAsync();
		Task<ActionResult<ExpenseGroup>> GetExpenseGroupAsync(int id);
		Task<ActionResult<ExpenseGroup>> CreateExpenseGroupAsync(ExpenseGroup expenseGroup);
		Task<IActionResult> UpdateExpenseGroupAsync(int id, ExpenseGroup expenseGroup);
		Task<IActionResult> DeleteExpenseGroupByIdAsync(int id);
	}
}
