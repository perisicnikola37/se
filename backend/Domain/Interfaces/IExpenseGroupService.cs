using Contracts.Dto;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Domain.Interfaces
{
	public interface IExpenseGroupService
	{
		Task<IEnumerable<ExpenseGroupDto>> GetExpenseGroupsAsync(ControllerBase controller);
		Task<ActionResult<ExpenseGroup>> GetExpenseGroupAsync(int id);
		Task<ActionResult<ExpenseGroup>> CreateExpenseGroupAsync(ExpenseGroup expenseGroup, ControllerBase controller);
		Task<IActionResult> UpdateExpenseGroupAsync(int id, ExpenseGroup expenseGroup, ControllerBase controller);
		Task<IActionResult> DeleteExpenseGroupByIdAsync(int id);
	}
}
