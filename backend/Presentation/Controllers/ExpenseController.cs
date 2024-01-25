using Contracts.Filter;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace Presentation.Controllers;

[Route("api/expenses")]
[ApiController]
public class ExpenseController(ExpenseService _expenseService) : ControllerBase
{
	// GET: api/Expense
	[HttpGet]
	public async Task<IActionResult> GetExpensesAsync([FromQuery] PaginationFilter filter)
	{
		return Ok(await _expenseService.GetExpensesAsync(filter));
	}

	// GET: api/Expense/latest/5
	[HttpGet("latest/5")]
	public async Task<ActionResult<IEnumerable<Expense>>> GetLatestExpensesAsync()
	{
		return Ok(await _expenseService.GetLatestExpensesAsync());
	}

	// GET: api/Expense/total-amount
	[HttpGet("total-amount")]
	public ActionResult<int> GetTotalAmountOfExpensesAsync()
	{
		return Ok(_expenseService.GetTotalAmountOfExpensesAsync());
	}

	// GET: api/Expense/5
	[HttpGet("{id}")]
	public async Task<IActionResult> GetExpenseAsync(int id)
	{
		var expense = await _expenseService.GetExpenseAsync(id);

		return expense != null ? Ok(expense) : NotFound();
	}

	// PUT: api/Expense/5
	[HttpPut("{id}")]
	public async Task<IActionResult> PutExpenseAsync(int id, Expense expense)
	{
		return await _expenseService.UpdateExpenseAsync(id, expense, this);
	}

	// POST: api/Expense
	[HttpPost]
	public async Task<ActionResult<Expense>> PostExpenseAsync(Expense expense)
	{
		return await _expenseService.CreateExpenseAsync(expense, this);
	}

	// DELETE: api/Expense/5
	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteExpenseAsync(int id)
	{
		return await _expenseService.DeleteExpenseByIdAsync(id);
	}
}