using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Service
{
    public interface IExpenseGroupService
    {
        Task<IEnumerable<object>> GetExpenseGroupsAsync();
        Task<ActionResult<ExpenseGroup>> GetExpenseGroupAsync(int id);
        Task<ActionResult<ExpenseGroup>> CreateExpenseGroupAsync(ExpenseGroup expenseGroup, ControllerBase controller);
        Task<IActionResult> UpdateExpenseGroupAsync(int id, ExpenseGroup expenseGroup);
        Task<IActionResult> DeleteExpenseGroupByIdAsync(int id);
    }
}
