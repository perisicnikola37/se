using Contracts.Dto;
using Domain.Models;
using Infrastructure.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Service;

public class ReminderService(DatabaseContext _context, ILogger<ReminderService> _logger, GetAuthenticatedUserIdService getAuthenticatedUserIdService)
{
	private readonly DatabaseContext _context = _context;
	private readonly ILogger<ReminderService> _logger = _logger;
	private readonly GetAuthenticatedUserIdService getAuthenticatedUserIdService = getAuthenticatedUserIdService;
	public async Task<ActionResult<ReminderDTO>> GetRemindersAsync()
	{
		try
		{
			var reminders = await _context.Reminders.OrderByDescending(e => e.CreatedAt).ToListAsync();

			return new ReminderDTO
			{
				Reminders = reminders,
			};
		}
		catch (Exception ex)
		{
			_logger.LogError($"GetRemindersAsync: An error occurred. Error: {ex.Message}");
			throw;
		}
	}

	public async Task<ActionResult<SingleReminderDTO>> GetReminderAsync(int id)
	{
		try
		{
			var reminder = await _context.Reminders.FindAsync(id);

			if (reminder == null) return new NotFoundResult();

			return new SingleReminderDTO
			{
				Reminder = new[] { reminder },
			};
		}
		catch (Exception ex)
		{
			_logger.LogError($"GetReminderAsync: An error occurred. Error: {ex.Message}");
			throw;
		}
	}

	public async Task<IActionResult> UpdateReminderAsync(int id, Reminder reminder, ControllerBase controller)
	{
		try
		{
			if (id != reminder.Id) return new BadRequestResult();

			if (!ReminderExists(id)) return new NotFoundResult();

			// Update the reminder
			_context.Entry(reminder).State = EntityState.Modified;
			await _context.SaveChangesAsync();

			return new NoContentResult();
		}
		catch (DbUpdateConcurrencyException ex)
		{
			_logger.LogError($"UpdateReminderAsync: Concurrency conflict occurred. Error: {ex.Message}");
			if (!ReminderExists(id))
				return new NotFoundResult();
			throw;
		}
		catch (Exception ex)
		{
			_logger.LogError($"UpdateReminderAsync: An error occurred. Error: {ex.Message}");
			throw;
		}
	}

	public async Task<ActionResult<Reminder>> CreateReminderAsync(Reminder reminder, ControllerBase controller)
	{
		try
		{
			// var validationResult = await _validator.ValidateAsync(reminder);

			// if (!validationResult.IsValid)
			// {
			// 	return new BadRequestObjectResult(validationResult.Errors);
			// }

			var userId = getAuthenticatedUserIdService.GetUserId(controller.User);

			if (userId == null)
			{
				return new UnauthorizedResult();
			}

			_context.Reminders.Add(reminder);
			await _context.SaveChangesAsync();

			return new CreatedAtActionResult("GetReminder", "Reminder", new { id = reminder.Id }, reminder);
		}
		catch (Exception ex)
		{
			_logger.LogError($"CreateReminderAsync: An error occurred. Error: {ex.Message}");
			throw;
		}
	}

	public async Task<IActionResult> DeleteReminderAsync(int id)
	{
		try
		{
			var reminder = await _context.Reminders.FindAsync(id);
			if (reminder == null)
			{
				_logger.LogWarning($"DeleteReminderAsync: Reminder with ID {id} not found.");
				return new NotFoundResult();
			}

			_context.Reminders.Remove(reminder);
			await _context.SaveChangesAsync();

			_logger.LogInformation($"DeleteReminderAsync: Reminder with ID {id} successfully deleted.");

			return new NoContentResult();
		}
		catch (DbUpdateConcurrencyException ex)
		{
			_logger.LogError($"DeleteReminderAsync: Concurrency conflict occurred. Error: {ex.Message}");
		}
		catch (Exception ex)
		{
			_logger.LogError($"DeleteReminderAsync: An error occurred. Error: {ex.Message}");
			throw;
		}

		return new NoContentResult();
	}

	private bool ReminderExists(int id)
	{
		return _context.Reminders.Any(e => e.Id == id);
	}

}
