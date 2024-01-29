using Contracts.Filter;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Presentation.Controllers;

[Route("api/incomes")]
[ApiController]
[EnableRateLimiting("fixed")]
public class IncomeController(IIncomeService incomeService) : ControllerBase
{
	// GET: api/Income
	[HttpGet]
	public async Task<IActionResult> GetIncomesAsync([FromQuery] PaginationFilterDto filter)
	{
		return Ok(await incomeService.GetIncomesAsync(filter, this));
	}

	// GET: api/Income/latest/5
	[HttpGet("latest/5")]
	public async Task<ActionResult<IEnumerable<Income>>> GetLatestIncomesAsync()
	{
		return Ok(await incomeService.GetLatestIncomesAsync());
	}

	// GET: api/Income/total-amount
	[HttpGet("total-amount")]
	public ActionResult<int> GetTotalAmountOfIncomesAsync()
	{
		return Ok(incomeService.GetTotalAmountOfIncomesAsync());
	}

	// GET: api/Income/5
	[HttpGet("{id}")]
	public async Task<ActionResult<Income>> GetIncomeAsync(int id)
	{
		return await incomeService.GetIncomeAsync(id);
	}

	// PUT: api/Income/5
	[HttpPut("{id}")]
	[Authorize("IncomeOwnerPolicy")]
	public async Task<IActionResult> PutIncomeAsync(int id, Income income)
	{
		return await incomeService.UpdateIncomeAsync(id, income, this);
	}

	// POST: api/Income
	[HttpPost]
	public async Task<ActionResult<Income>> PostIncomeAsync(Income income)
	{
		return await incomeService.CreateIncomeAsync(income, this);
	}

	// DELETE: api/Income/5
	[HttpDelete("{id}")]
	[Authorize("IncomeOwnerPolicy")]
	public async Task<IActionResult> DeleteIncomeAsync(int id)
	{
		return await incomeService.DeleteIncomeByIdAsync(id);
	}
}