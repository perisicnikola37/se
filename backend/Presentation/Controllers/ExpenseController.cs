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
	[HttpGet]
	public async Task<IActionResult> GetExpensesAsync([FromQuery] PaginationFilterDto filter)
	{
		return Ok(await expenseService.GetExpensesAsync(filter, this));
	}

	[HttpGet("latest/5")]
	public async Task<ActionResult<IEnumerable<Expense>>> GetLatestExpensesAsync()
	{
		return Ok(await expenseService.GetLatestExpensesAsync(this));
	}

	[HttpGet("total/amount")]
	public ActionResult<int> GetTotalAmountOfExpensesAsync()
	{
		return Ok(expenseService.GetTotalAmountOfExpensesAsync());
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<Expense>> GetExpenseAsync(int id)
	{
		return await expenseService.GetExpenseAsync(id);
	}

	[HttpPut("{id}")]
	[Authorize("ExpenseOwnerPolicy")]
	public async Task<IActionResult> PutExpenseAsync(int id, Expense expense)
	{
		return await expenseService.UpdateExpenseAsync(id, expense, this);
	}

	[HttpPost]
	public async Task<ActionResult<Expense>> PostExpenseAsync(Expense expense)
	{
		return await expenseService.CreateExpenseAsync(expense, this);
	}

	[HttpDelete("{id}")]
	[Authorize("ExpenseOwnerPolicy")]
	public async Task<IActionResult> DeleteExpenseAsync(int id)
	{
		return await expenseService.DeleteExpenseByIdAsync(id);
	}

	[HttpDelete]
	public async Task<IActionResult> DeleteAllExpensesAsync()
	{
		return await expenseService.DeleteAllExpensesAsync(this);
	}
}