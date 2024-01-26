using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Presentation.Controllers;

[Route("api/expenses/groups")]
[ApiController]
[EnableRateLimiting("fixed")]
public class ExpenseGroupController(IExpenseGroupService expenseGroupService) : ControllerBase
{
	// GET: api/ExpenseGroup
	
	public IActionResult GetExpenseGroup(int id)
	{
		throw new NotImplementedException();
	}
	
	[HttpGet]
	public async Task<ActionResult<IEnumerable<object>>> GetExpenseGroupsAsync()
	{
		return Ok(await expenseGroupService.GetExpenseGroupsAsync());
	}

	// GET: api/ExpenseGroup/5
	[HttpGet("{id}")]
	public async Task<ActionResult<ExpenseGroup>> GetExpenseGroupAsync(int id)
	{
		return await expenseGroupService.GetExpenseGroupAsync(id);
	}

	// PUT: api/ExpenseGroup/5
	[HttpPut("{id}")]
	public async Task<IActionResult> PutExpenseGroupAsync(int id, ExpenseGroup expenseGroup)
	{
		return await expenseGroupService.UpdateExpenseGroupAsync(id, expenseGroup);
	}

	// POST: api/ExpenseGroup
	[HttpPost]
	public async Task<ActionResult<ExpenseGroup>> PostExpenseGroupAsync(ExpenseGroup expenseGroup)
	{
		return await expenseGroupService.CreateExpenseGroupAsync(expenseGroup, this);
	}

	// DELETE: api/ExpenseGroup/5
	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteExpenseGroupByIdAsync(int id)
	{
		return await expenseGroupService.DeleteExpenseGroupByIdAsync(id);
	}
}