using Domain.Models;
using Infrastructure.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReminderController(DatabaseContext context) : ControllerBase
{
    // GET: api/Reminder
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Reminder>>> GetReminders()
    {
        var reminders = await context.Reminders.OrderByDescending(e => e.CreatedAt).ToListAsync();

        if (reminders.Count != 0)
            return reminders;
        return NotFound();
    }

    // GET: api/Reminder/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Reminder>> GetReminder(int id)
    {
        var reminder = await context.Reminders.FindAsync(id);

        if (reminder == null) return NotFound();

        return reminder;
    }

    // PUT: api/Reminder/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutReminder(int id, Reminder reminder)
    {
        if (id != reminder.Id) return BadRequest();

        context.Entry(reminder).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ReminderExists(id))
                return NotFound();
            throw;
        }

        return NoContent();
    }

    // POST: api/Reminder
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Reminder>> PostReminder(Reminder reminder)
    {
        context.Reminders.Add(reminder);
        await context.SaveChangesAsync();

        return CreatedAtAction("GetReminder", new { id = reminder.Id }, reminder);
    }

    // DELETE: api/Reminder/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReminder(int id)
    {
        var reminder = await context.Reminders.FindAsync(id);
        if (reminder == null) return NotFound();

        context.Reminders.Remove(reminder);
        await context.SaveChangesAsync();

        return NoContent();
    }

    private bool ReminderExists(int id)
    {
        return context.Reminders.Any(e => e.Id == id);
    }
}