using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Contexts;
using Domain.Models;

namespace Presentation.Controllers;
    [Route("api/[controller]")]
    [ApiController]
    public class ReminderController : ControllerBase
    {
        private readonly MainDatabaseContext _context;

        public ReminderController(MainDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Reminder
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reminder>>> GetReminders()
        {
            var reminders = await _context.Reminders.OrderByDescending(e => e.Created_at).ToListAsync();

             if (reminders.Count != 0)
            {
                return reminders;
            }
            else
            {
                return NotFound();
            }
        }

        // GET: api/Reminder/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reminder>> GetReminder(int id)
        {
            var reminder = await _context.Reminders.FindAsync(id);

            if (reminder == null)
            {
                return NotFound();
            }

            return reminder;
        }

        // PUT: api/Reminder/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReminder(int id, Reminder reminder)
        {
            if (id != reminder.Id)
            {
                return BadRequest();
            }

            _context.Entry(reminder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReminderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Reminder
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Reminder>> PostReminder(Reminder reminder)
        {
            _context.Reminders.Add(reminder);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReminder", new { id = reminder.Id }, reminder);
        }

        // DELETE: api/Reminder/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReminder(int id)
        {
            var reminder = await _context.Reminders.FindAsync(id);
            if (reminder == null)
            {
                return NotFound();
            }

            _context.Reminders.Remove(reminder);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReminderExists(int id)
        {
            return _context.Reminders.Any(e => e.Id == id);
        }
    }

