using Contracts.Filter;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Presentation.Controllers;

[Route("api/expenses")]
[ApiController]
[EnableRateLimiting("fixed")]
public class ExpenseController(IExpenseService expenseService) : ControllerBase
{
	//TODO remove comments
	// GET: api/Expense
	[HttpGet]
	public async Task<IActionResult> GetExpensesAsync([FromQuery] PaginationFilterDto filter)
	{
		return Ok(await expenseService.GetExpensesAsync(filter, this));
	}

	// GET: api/Expense/latest/5
	[HttpGet("latest/5")]
	public async Task<ActionResult<IEnumerable<Expense>>> GetLatestExpensesAsync()
	{
		return Ok(await expenseService.GetLatestExpensesAsync());
	}

	// GET: api/Expense/total-amount
	[HttpGet("total-amount")]
	public ActionResult<int> GetTotalAmountOfExpensesAsync()
	{
		return Ok(expenseService.GetTotalAmountOfExpensesAsync());
	}

	// GET: api/Expense/5
	[HttpGet("{id}")]
	public async Task<ActionResult<Expense>> GetExpenseAsync(int id)
	{
		return await expenseService.GetExpenseAsync(id);
	}

	// PUT: api/Expense/5
	[HttpPut("{id}")]
	[Authorize("ExpenseOwnerPolicy")]
	public async Task<IActionResult> PutExpenseAsync(int id, Expense expense)
	{
		return await expenseService.UpdateExpenseAsync(id, expense, this);
	}

	// POST: api/Expense
	[HttpPost]
	public async Task<ActionResult<Expense>> PostExpenseAsync(Expense expense)
	{
		return await expenseService.CreateExpenseAsync(expense, this);
	}

	// DELETE: api/Expense/5
	[HttpDelete("{id}")]
	[Authorize("ExpenseOwnerPolicy")]
	public async Task<IActionResult> DeleteExpenseAsync(int id)
	{
		return await expenseService.DeleteExpenseByIdAsync(id);
	}
}