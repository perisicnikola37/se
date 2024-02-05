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
	[HttpGet]
	public async Task<IActionResult> GetIncomesAsync([FromQuery] PaginationFilterDto filter)
	{
		return Ok(await incomeService.GetIncomesAsync(filter, this));
	}

	[HttpGet("latest/5")]
	public async Task<ActionResult<object>> GetLatestIncomesAsync()
	{
		return Ok(await incomeService.GetLatestIncomesAsync(this));
	}

	[HttpGet("total-amount")]
	public ActionResult<int> GetTotalAmountOfIncomesAsync()
	{
		return Ok(incomeService.GetTotalAmountOfIncomesAsync());
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<Income>> GetIncomeAsync(int id)
	{
		return await incomeService.GetIncomeAsync(id);
	}

	[HttpPut("{id}")]
	[Authorize("IncomeOwnerPolicy")]
	public async Task<IActionResult> PutIncomeAsync(int id, Income income)
	{
		return await incomeService.UpdateIncomeAsync(id, income, this);
	}

	[HttpPost]
	public async Task<ActionResult<Income>> PostIncomeAsync(Income income)
	{
		return await incomeService.CreateIncomeAsync(income, this);
	}

	[HttpDelete]
	public async Task<IActionResult> DeleteAllIncomesAsync()
	{
		return await incomeService.DeleteAllIncomesAsync(this);
	}

	[HttpDelete("{id}")]
	[Authorize("IncomeOwnerPolicy")]
	public async Task<IActionResult> DeleteIncomeAsync(int id)
	{
		return await incomeService.DeleteIncomeByIdAsync(id);
	}
}