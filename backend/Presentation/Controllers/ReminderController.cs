using Contracts.Dto.Reminders;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Presentation.Controllers;

[Route("api/reminders")]
[ApiController]
[EnableRateLimiting("fixed")]
public class ReminderController(IReminderService reminderService) : ControllerBase
{
	// GET: api/Reminder
	[HttpGet]
	public async Task<ActionResult<IEnumerable<ReminderDto>>> GetRemindersAsync()
	{
		return Ok(await reminderService.GetRemindersAsync());
	}

	// GET: api/Reminder/5
	[HttpGet("{id}")]
	public async Task<ActionResult<SingleReminderDto>> GetReminder(int id)
	{
		return await reminderService.GetReminderAsync(id);
	}

	// PUT: api/Reminder/5
	[HttpPut("{id}")]
	public async Task<IActionResult> PutReminderAsync(int id, Reminder reminder)
	{
		return await reminderService.UpdateReminderAsync(id, reminder, this);
	}

	// POST: api/Reminder
	[HttpPost]
	public async Task<ActionResult<Reminder>> PostReminderAsync(Reminder reminder)
	{
		return await reminderService.CreateReminderAsync(reminder, this);
	}

	// DELETE: api/Reminder/5
	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteReminderAsync(int id)
	{
		return await reminderService.DeleteReminderAsync(id);
	}
}