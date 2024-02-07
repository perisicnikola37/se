using Contracts.Dto;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Presentation.Controllers;

[Route("api/reminders")]
[ApiController]
[EnableRateLimiting("fixed")]
public class ReminderController(IReminderService reminderService) : ControllerBase
{
	[HttpGet]
	public async Task<ActionResult<IEnumerable<Reminder>>> GetRemindersAsync()
	{
		return Ok(await reminderService.GetRemindersAsync());
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<SingleReminderDto>> GetReminder(int id)
	{
		return await reminderService.GetReminderAsync(id);
	}

	[HttpPut("{id}")]
	public async Task<IActionResult> PutReminderAsync(int id, Reminder reminder)
	{
		return await reminderService.UpdateReminderAsync(id, reminder);
	}

	[HttpPost]
	public async Task<ActionResult<Reminder>> PostReminderAsync(Reminder reminder)
	{
		return await reminderService.CreateReminderAsync(reminder);
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteReminderAsync(int id)
	{
		return await reminderService.DeleteReminderAsync(id);
	}
}