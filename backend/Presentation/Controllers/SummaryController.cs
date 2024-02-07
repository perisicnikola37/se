using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Presentation.Controllers;

[ApiController]
[Route("api/summary")]
[EnableRateLimiting("fixed")]
public class SummaryController(ISummaryService summaryService) : ControllerBase
{
	[HttpGet("last-week")]
	public async Task<IActionResult> GetLast7DaysIncomesAndExpensesAsync()
	{
		return Ok(await summaryService.GetLast7DaysIncomesAndExpensesAsync());
	}
}
