using Contracts.Dto.Reminders;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Domain.Interfaces;

public interface IReminderService
{
    Task<ActionResult<ReminderDto>> GetRemindersAsync();
    Task<ActionResult<SingleReminderDto>> GetReminderAsync(int id);
    Task<IActionResult> UpdateReminderAsync(int id, Reminder reminder, ControllerBase controller);
    Task<ActionResult<Reminder>> CreateReminderAsync(Reminder reminder, ControllerBase controller);
    Task<IActionResult> DeleteReminderAsync(int id);
}