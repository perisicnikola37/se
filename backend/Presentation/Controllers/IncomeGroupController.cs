using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IncomeGroupController(IncomeGroupService _incomeGroupService) : ControllerBase
{
	// GET: api/IncomeGroup
	[HttpGet]
	public async Task<ActionResult<IEnumerable<object>>> GetIncomeGroupsAsync()
	{
		return Ok(await _incomeGroupService.GetIncomeGroupsAsync());
	}

	// GET: api/IncomeGroup/5
	[HttpGet("{id}")]
	public async Task<ActionResult<IncomeGroup>> GetIncomeGroupAsync(int id)
	{
		return await _incomeGroupService.GetIncomeGroupAsync(id);
	}

	// PUT: api/IncomeGroup/5
	[HttpPut("{id}")]
	public async Task<IActionResult> PutIncomeGroupAsync(int id, IncomeGroup incomeGroup)
	{
		return await _incomeGroupService.UpdateIncomeGroupAsync(id, incomeGroup);
	}

	// POST: api/IncomeGroup
	[HttpPost]
	public async Task<ActionResult<IncomeGroup>> PostIncomeGroupAsync(IncomeGroup incomeGroup)
	{
		return await _incomeGroupService.CreateIncomeGroupAsync(incomeGroup, this);
	}

	// DELETE: api/IncomeGroup/5
	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteIncomeGroupByIdAsync(int id)
	{
		return await _incomeGroupService.DeleteIncomeGroupByIdAsync(id);
	}
}