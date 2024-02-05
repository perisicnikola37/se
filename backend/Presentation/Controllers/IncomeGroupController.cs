using Contracts.Dto;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Presentation.Controllers;

[Route("api/incomes/groups")]
[ApiController]
[EnableRateLimiting("fixed")]
public class IncomeGroupController(IIncomeGroupService incomeGroupService) : ControllerBase
{
	[HttpGet]
	public async Task<ActionResult<IEnumerable<IncomeGroupDto>>> GetIncomeGroupsAsync()
	{
		return Ok(await incomeGroupService.GetIncomeGroupsAsync(this));
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<IncomeGroup>> GetIncomeGroupAsync(int id)
	{
		return await incomeGroupService.GetIncomeGroupAsync(id);
	}

	[HttpPut("{id}")]
	[Authorize("IncomeGroupOwnerPolicy")]
	public async Task<IActionResult> PutIncomeGroupAsync(int id, IncomeGroup incomeGroup)
	{
		return await incomeGroupService.UpdateIncomeGroupAsync(id, incomeGroup, this);
	}

	[HttpPost]
	public async Task<ActionResult<IncomeGroup>> PostIncomeGroupAsync(IncomeGroup incomeGroup)
	{
		return await incomeGroupService.CreateIncomeGroupAsync(incomeGroup, this);
	}

	[HttpDelete("{id}")]
	[Authorize("IncomeGroupOwnerPolicy")]
	public async Task<IActionResult> DeleteIncomeGroupByIdAsync(int id)
	{
		return await incomeGroupService.DeleteIncomeGroupByIdAsync(id);
	}
}