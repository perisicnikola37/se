using Contracts.Dto;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using FluentValidation;
using Infrastructure.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Service;

public class ReminderService(DatabaseContext context, ILogger<ReminderService> logger, IGetAuthenticatedUserIdService getAuthenticatedUserId, IValidator<Reminder> validator): IReminderService
{
	public async Task<ActionResult<ReminderDTO>> GetRemindersAsync()
	{
		try
		{
			var reminders = await context.Reminders.OrderByDescending(e => e.CreatedAt).ToListAsync();

			return new ReminderDTO
			{
				Reminders = reminders,
			};
		}
		catch (Exception ex)
		{
			logger.LogError($"GetRemindersAsync: An error occurred. Error: {ex.Message}");
			throw;
		}
	}

	public async Task<ActionResult<SingleReminderDTO>> GetReminderAsync(int id)
	{
		try
		{
			var reminder = await context.Reminders.FindAsync(id);

			if (reminder == null) return new NotFoundResult();

			return new SingleReminderDTO
			{
				Reminder = new[] { reminder },
			};
		}
		catch (Exception ex)
		{
			logger.LogError($"GetReminderAsync: An error occurred. Error: {ex.Message}");
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
			context.Entry(reminder).State = EntityState.Modified;
			await context.SaveChangesAsync();

			return new NoContentResult();
		}
		catch (ConflictException ex)
		{
			logger.LogError($"UpdateReminderAsync: Concurrency conflict occurred. Error: {ex.Message}");
			if (!ReminderExists(id))
				return new NotFoundResult();
			throw new ConflictException("ReminderService.cs");
		}
		catch (Exception ex)
		{
			logger.LogError($"UpdateReminderAsync: An error occurred. Error: {ex.Message}");
			throw;
		}
	}

	public async Task<ActionResult<Reminder>> CreateReminderAsync(Reminder reminder, ControllerBase controller)
	{
		try
		{
			var validationResult = await validator.ValidateAsync(reminder);
			if (!validationResult.IsValid) return new BadRequestObjectResult(validationResult.Errors);

			var userId = getAuthenticatedUserId.GetUserId(controller.User);

			if (userId == null)
			{
				return new UnauthorizedResult();
			}

			context.Reminders.Add(reminder);
			await context.SaveChangesAsync();

			return new CreatedAtActionResult("GetReminder", "Reminder", new { id = reminder.Id }, reminder);
		}
		catch (Exception ex)
		{
			logger.LogError($"CreateReminderAsync: An error occurred. Error: {ex.Message}");
			throw;
		}
	}

	public async Task<IActionResult> DeleteReminderAsync(int id)
	{
		try
		{
			var reminder = await context.Reminders.FindAsync(id);
			if (reminder == null)
			{
				logger.LogWarning($"DeleteReminderAsync: Reminder with ID {id} not found.");
				return new NotFoundResult();
			}

			context.Reminders.Remove(reminder);
			await context.SaveChangesAsync();

			logger.LogInformation($"DeleteReminderAsync: Reminder with ID {id} successfully deleted.");

			return new NoContentResult();
		}
		catch (ConflictException ex)
		{
			logger.LogError($"DeleteReminderAsync: Concurrency conflict occurred. Error: {ex.Message}");
			throw new ConflictException("ReminderService.cs");
		}
		catch (Exception ex)
		{
			logger.LogError($"DeleteReminderAsync: An error occurred. Error: {ex.Message}");
			throw;
		}
	}

	private bool ReminderExists(int id)
	{
		return context.Reminders.Any(e => e.Id == id);
	}

}
