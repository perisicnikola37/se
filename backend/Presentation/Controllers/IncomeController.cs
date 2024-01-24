using Contracts.Filter;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IncomeController(IncomeService _incomeService) : ControllerBase
{
	// GET: api/Income
	[HttpGet]
	public async Task<IActionResult> GetIncomesAsync([FromQuery] PaginationFilter filter)
	{
		return Ok(await _incomeService.GetIncomesAsync(filter));
	}

	// GET: api/Income/latest/5
	[HttpGet("latest/5")]
	public async Task<ActionResult<IEnumerable<Income>>> GetLatestIncomesAsync()
	{
		return Ok(await _incomeService.GetLatestIncomesAsync());
	}

	// GET: api/Income/total-amount
	[HttpGet("total-amount")]
	public ActionResult<int> GetTotalAmountOfIncomesAsync()
	{
		return Ok(_incomeService.GetTotalAmountOfIncomesAsync());
	}

	// GET: api/Income/5
	[HttpGet("{id}")]
	public async Task<IActionResult> GetIncomeAsync(int id)
	{
		var income = await _incomeService.GetIncomeAsync(id);

		return income != null ? Ok(income) : NotFound();
	}

	// PUT: api/Income/5
	[HttpPut("{id}")]
	public async Task<IActionResult> PutIncomeAsync(int id, Income income)
	{
		return await _incomeService.UpdateIncomeAsync(id, income);
	}

	// POST: api/Income
	[HttpPost]
	public async Task<ActionResult<Income>> PostIncomeAsync(Income income)
	{
		return await _incomeService.CreateIncomeAsync(income, this);
	}

	// DELETE: api/Income/5
	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteIncomeAsync(int id)
	{
		return await _incomeService.DeleteIncomeByIdAsync(id);
	}
}