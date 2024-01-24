using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExpenseGroupController(ExpenseGroupService _expenseGroupService) : ControllerBase
{
	// GET: api/ExpenseGroup
	[HttpGet]
	public async Task<ActionResult<IEnumerable<object>>> GetExpenseGroupsAsync()
	{
		return Ok(await _expenseGroupService.GetExpenseGroupsAsync());
	}

	// GET: api/ExpenseGroup/5
	[HttpGet("{id}")]
	public async Task<ActionResult<ExpenseGroup>> GetExpenseGroupAsync(int id)
	{
		return await _expenseGroupService.GetExpenseGroupAsync(id);
	}

	// PUT: api/ExpenseGroup/5
	[HttpPut("{id}")]
	public async Task<IActionResult> PutExpenseGroupAsync(int id, ExpenseGroup expenseGroup)
	{
		return await _expenseGroupService.UpdateExpenseGroupAsync(id, expenseGroup);
	}

	// POST: api/ExpenseGroup
	[HttpPost]
	public async Task<ActionResult<ExpenseGroup>> PostExpenseGroupAsync(ExpenseGroup expenseGroup)
	{
		return await _expenseGroupService.CreateExpenseGroupAsync(expenseGroup, this);
	}

	// DELETE: api/ExpenseGroup/5
	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteExpenseGroupByIdAsync(int id)
	{
		return await _expenseGroupService.DeleteExpenseGroupByIdAsync(id);
	}
}