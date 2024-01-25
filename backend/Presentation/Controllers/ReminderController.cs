using Contracts.Dto;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Service;

namespace Presentation.Controllers;

[Route("api/reminders")]
[ApiController]
[EnableRateLimiting("fixed")]
public class ReminderController(ReminderService _reminderService) : ControllerBase
{
	// GET: api/Reminder
	[HttpGet]
	public async Task<ActionResult<IEnumerable<ReminderDTO>>> GetRemindersAsync()
	{
		return Ok(await _reminderService.GetRemindersAsync());
	}

	// GET: api/Reminder/5
	[HttpGet("{id}")]
	public async Task<ActionResult<SingleReminderDTO>> GetReminder(int id)
	{
		return await _reminderService.GetReminderAsync(id);
	}

	// PUT: api/Reminder/5
	[HttpPut("{id}")]
	public async Task<IActionResult> PutReminderAsync(int id, Reminder reminder)
	{
		return await _reminderService.UpdateReminderAsync(id, reminder, this);
	}

	// POST: api/Reminder
	[HttpPost]
	public async Task<ActionResult<Reminder>> PostReminderAsync(Reminder reminder)
	{
		return await _reminderService.CreateReminderAsync(reminder, this);
	}

	// DELETE: api/Reminder/5
	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteReminderAsync(int id)
	{
		return await _reminderService.DeleteReminderAsync(id);
	}
}