using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/summary")]
public class SummaryController(ISummaryService summaryService) : ControllerBase
{
	[HttpGet("last-week")]
	public async Task<IActionResult> GetLast7DaysIncomesAndExpensesAsync()
	{
		return Ok(await summaryService.GetLast7DaysIncomesAndExpensesAsync(this));
	}
}
