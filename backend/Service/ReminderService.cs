using Contracts.Dto;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Persistence;

namespace Service;

public class ReminderService(
	ILogger<ReminderService> logger,
	IGetAuthenticatedUserIdService getAuthenticatedUserId,
	IValidator<Reminder> validator,
	ReminderRepository reminderRepository,
	IHttpContextAccessor httpContextAccessor) : IReminderService
{
	public async Task<IEnumerable<Reminder>> GetRemindersAsync()
	{
		try
		{
			var reminders = await reminderRepository.GetRemindersAsync();

			return reminders;
		}
		catch (Exception ex)
		{
			logger.LogError($"GetRemindersAsync: An error occurred. Error: {ex.Message}");
			throw new DatabaseException();
		}
	}

	public async Task<ActionResult<SingleReminderDto>> GetReminderAsync(int id)
	{
		try
		{
			var reminder = await reminderRepository.GetReminderAsync(id);

			if (reminder == null)
			{
				logger.LogWarning($"GetReminderAsync: Reminder with ID {id} not found.");
				return new NotFoundResult();
			}

			return new SingleReminderDto
			{
				Reminder = new[] { reminder }
			};
		}
		catch (Exception ex)
		{
			logger.LogError($"GetReminderAsync: An error occurred. Error: {ex.Message}");
			throw new DatabaseException();
		}
	}

	public async Task<ActionResult<Reminder>> CreateReminderAsync(Reminder reminder)
	{
		try
		{
			var validationResult = await validator.ValidateAsync(reminder);
			if (!validationResult.IsValid) return new BadRequestObjectResult(validationResult.Errors);

			var userId = getAuthenticatedUserId.GetUserId(httpContextAccessor.HttpContext.User);

			if (userId == null) return new UnauthorizedResult();

			await reminderRepository.CreateReminderAsync(reminder);

			return new CreatedAtActionResult("GetReminder", "Reminder", new { id = reminder.Id }, reminder);
		}
		catch (Exception ex)
		{
			logger.LogError($"CreateReminderAsync: An error occurred. Error: {ex.Message}");
			throw new DatabaseException();
		}
	}

	public async Task<IActionResult> UpdateReminderAsync(int id, Reminder reminder)
	{
		try
		{
			if (id != reminder.Id) return new BadRequestResult();

			if (!await reminderRepository.ReminderExistsAsync(id)) return new NotFoundResult();

			await reminderRepository.UpdateReminderAsync(reminder);

			return new NoContentResult();
		}
		catch (ConflictException ex)
		{
			logger.LogError($"UpdateReminderAsync: Concurrency conflict occurred. Error: {ex.Message}");
			if (!await reminderRepository.ReminderExistsAsync(id))
				return new NotFoundResult();
			throw new ConflictException();
		}
		catch (Exception ex)
		{
			logger.LogError($"UpdateReminderAsync: An error occurred. Error: {ex.Message}");
			throw new DatabaseException();
		}
	}

	public async Task<IActionResult> DeleteReminderAsync(int id)
	{
		try
		{
			if (!await reminderRepository.ReminderExistsAsync(id))
			{
				logger.LogWarning($"DeleteReminderAsync: Reminder with ID {id} not found.");
				return new NotFoundResult();
			}

			await reminderRepository.DeleteReminderAsync(id);

			logger.LogInformation($"DeleteReminderAsync: Reminder with ID {id} successfully deleted.");

			return new NoContentResult();
		}
		catch (ConflictException ex)
		{
			logger.LogError($"DeleteReminderAsync: Concurrency conflict occurred. Error: {ex.Message}");
			throw new ConflictException();
		}
		catch (Exception ex)
		{
			logger.LogError($"DeleteReminderAsync: An error occurred. Error: {ex.Message}");
			throw new DatabaseException();
		}
	}
}