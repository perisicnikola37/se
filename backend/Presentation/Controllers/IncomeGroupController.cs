using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Presentation.Controllers;

[Route("api/incomes/groups")]
[ApiController]
[EnableRateLimiting("fixed")]
public class IncomeGroupController(IIncomeGroupService incomeGroupService) : ControllerBase
{
	// GET: api/IncomeGroup
	[HttpGet]
	public async Task<ActionResult<IEnumerable<object>>> GetIncomeGroupsAsync()
	{
		return Ok(await incomeGroupService.GetIncomeGroupsAsync());
	}

	// GET: api/IncomeGroup/5
	[HttpGet("{id}")]
	public async Task<ActionResult<IncomeGroup>> GetIncomeGroupAsync(int id)
	{
		return await incomeGroupService.GetIncomeGroupAsync(id);
	}

	// PUT: api/IncomeGroup/5
	[HttpPut("{id}")]
	public async Task<IActionResult> PutIncomeGroupAsync(int id, IncomeGroup incomeGroup)
	{
		return await incomeGroupService.UpdateIncomeGroupAsync(id, incomeGroup);
	}

	// POST: api/IncomeGroup
	[HttpPost]
	public async Task<ActionResult<IncomeGroup>> PostIncomeGroupAsync(IncomeGroup incomeGroup)
	{
		return await incomeGroupService.CreateIncomeGroupAsync(incomeGroup, this);
	}

	// DELETE: api/IncomeGroup/5
	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteIncomeGroupByIdAsync(int id)
	{
		return await incomeGroupService.DeleteIncomeGroupByIdAsync(id);
	}
}