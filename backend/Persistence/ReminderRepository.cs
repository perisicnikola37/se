using Domain.Exceptions;
using Domain.Models;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Persistence;

public class ReminderRepository(DatabaseContext context, ILogger<Reminder> logger)
{
    public async Task<List<Reminder>> GetRemindersAsync()
    {
        try
        {
            return await context.Reminders.OrderByDescending(e => e.CreatedAt).ToListAsync();
        }
        catch (Exception ex)
        {
            logger.LogError($"GetRemindersAsync: An error occurred. Error: {ex.Message}");
            throw new DatabaseException("ReminderRepository.cs");
        }
    }

    public async Task<Reminder?> GetReminderAsync(int id)
    {
        try
        {
            return await context.Reminders.FindAsync(id);
        }
        catch (Exception ex)
        {
            logger.LogError($"GetReminderAsync: An error occurred. Error: {ex.Message}");
            throw new DatabaseException("ReminderRepository.cs");
        }
    }

    public async Task<bool> ReminderExistsAsync(int id)
    {
        try
        {
            return await context.Reminders.AnyAsync(e => e.Id == id);
        }
        catch (Exception ex)
        {
            logger.LogError($"ReminderExistsAsync: An error occurred. Error: {ex.Message}");
            throw new DatabaseException("ReminderRepository.cs");
        }
    }

    public async Task UpdateReminderAsync(Reminder reminder)
    {
        try
        {
            context.Entry(reminder).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            logger.LogError($"UpdateReminderAsync: An error occurred. Error: {ex.Message}");
            throw new DatabaseException("ReminderRepository.cs");
        }
    }

    public async Task CreateReminderAsync(Reminder reminder)
    {
        try
        {
            context.Reminders.Add(reminder);
            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            logger.LogError($"CreateReminderAsync: An error occurred. Error: {ex.Message}");
            throw new DatabaseException("ReminderRepository.cs");
        }
    }

    public async Task DeleteReminderAsync(int id)
    {
        try
        {
            var reminder = await context.Reminders.FindAsync(id);
            if (reminder != null) context.Reminders.Remove(reminder);
            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            logger.LogError($"DeleteReminderAsync: An error occurred. Error: {ex.Message}");
            throw new DatabaseException("ReminderRepository.cs");
        }
    }
}