using Contracts.Dto;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Domain.Interfaces
{
    public interface IReminderService
    {
        Task<ActionResult<ReminderDTO>> GetRemindersAsync();
        Task<ActionResult<SingleReminderDTO>> GetReminderAsync(int id);
        Task<IActionResult> UpdateReminderAsync(int id, Reminder reminder, ControllerBase controller);
        Task<ActionResult<Reminder>> CreateReminderAsync(Reminder reminder, ControllerBase controller);
        Task<IActionResult> DeleteReminderAsync(int id);
    }
}
