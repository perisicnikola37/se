using Contracts.Filter;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExpenseController(ExpenseService _expenseService)
	: ControllerBase
{
	// GET: api/Expense
	[HttpGet]
	public async Task<IActionResult> GetExpenses([FromQuery] PaginationFilter filter)
	{
		return Ok(await _expenseService.GetExpenses(filter));
	}

	// GET: api/Expense/latest/5
	[HttpGet("latest/5")]
	public async Task<ActionResult<IEnumerable<Expense>>> GetLatestExpenses()
	{
		return Ok(await _expenseService.GetLatestExpenses());
	}

	// GET: api/Expense/total-amount
	[HttpGet("total-amount")]
	public async Task<ActionResult<int>> GetTotalAmountOfExpenses()
	{
		return Ok(_expenseService.GetTotalAmountOfExpenses());
	}

	// GET: api/Expense/5
	[HttpGet("{id}")]
	public async Task<IActionResult> GetExpense(int id)
	{
		var expenseResponse = await _expenseService.GetExpenseById(id);

		if (expenseResponse == null)
		{
			return NotFound();
		}

		return Ok(expenseResponse);
	}

	// PUT: api/Expense/5
	[HttpPut("{id}")]
	public async Task<IActionResult> PutExpense(int id, Expense expense)
	{
		return await _expenseService.UpdateExpense(id, expense);
	}

	// POST: api/Expense
	[HttpPost]
	public async Task<ActionResult<Expense>> PostExpense(Expense expense)
	{
		return await _expenseService.CreateExpenseAsync(expense, this);
	}

	// DELETE: api/Expense/5
	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteExpense(int id)
	{
		return await _expenseService.DeleteExpenseById(id);
	}
}