using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Presentation.Controllers;

[Route("api/expenses/groups")]
[ApiController]
[EnableRateLimiting("fixed")]
public class ExpenseGroupController(IExpenseGroupService expenseGroupService) : ControllerBase
{
	[HttpGet]
	public async Task<ActionResult<IEnumerable<ExpenseGroup>>> GetExpenseGroupsAsync()
	{
		return Ok(await expenseGroupService.GetExpenseGroupsAsync(this));
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<ExpenseGroup>> GetExpenseGroupAsync(int id)
	{
		return await expenseGroupService.GetExpenseGroupAsync(id);
	}

	[HttpPut("{id}")]
	[Authorize("ExpenseGroupOwnerPolicy")]
	public async Task<IActionResult> PutExpenseGroupAsync(int id, ExpenseGroup expenseGroup)
	{
		return await expenseGroupService.UpdateExpenseGroupAsync(id, expenseGroup, this);
	}

	[HttpPost]
	public async Task<ActionResult<ExpenseGroup>> PostExpenseGroupAsync(ExpenseGroup expenseGroup)
	{
		return await expenseGroupService.CreateExpenseGroupAsync(expenseGroup, this);
	}

	[HttpDelete("{id}")]
	[Authorize("ExpenseGroupOwnerPolicy")]
	public async Task<IActionResult> DeleteExpenseGroupByIdAsync(int id)
	{
		return await expenseGroupService.DeleteExpenseGroupByIdAsync(id);
	}
}