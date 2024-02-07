using Contracts.Dto;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Domain.Interfaces;

public interface IReminderService
{
	Task<IEnumerable<Reminder>> GetRemindersAsync();
	Task<ActionResult<SingleReminderDto>> GetReminderAsync(int id);
	Task<IActionResult> UpdateReminderAsync(int id, Reminder reminder);
	Task<ActionResult<Reminder>> CreateReminderAsync(Reminder reminder);
	Task<IActionResult> DeleteReminderAsync(int id);
}