using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vega.classes;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseGroupController : ControllerBase
    {
        private readonly MyDBContext _context;

        public ExpenseGroupController(MyDBContext context)
        {
            _context = context;
        }

        // GET: api/ExpenseGroup
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExpenseGroup>>> GetExpense_groups()
        {
            var expense_groups = await _context.Expense_groups.OrderByDescending(e => e.Created_at).ToListAsync();

            if (expense_groups.Count != 0)
            {
                return expense_groups;
            }
            else
            {
                return NotFound();
            }
        }

        // GET: api/ExpenseGroup/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExpenseGroup>> GetExpenseGroup(int id)
        {
            var expenseGroup = await _context.Expense_groups.FindAsync(id);

            if (expenseGroup == null)
            {
                return NotFound();
            }

            return expenseGroup;
        }

        // PUT: api/ExpenseGroup/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExpenseGroup(int id, ExpenseGroup expenseGroup)
        {
            if (id != expenseGroup.Id)
            {
                return BadRequest();
            }

            _context.Entry(expenseGroup).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExpenseGroupExists(id))
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

        // POST: api/ExpenseGroup
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ExpenseGroup>> PostExpenseGroup(ExpenseGroup expenseGroup)
        {
            _context.Expense_groups.Add(expenseGroup);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExpenseGroup", new { id = expenseGroup.Id }, expenseGroup);
        }

        // DELETE: api/ExpenseGroup/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpenseGroup(int id)
        {
            var expenseGroup = await _context.Expense_groups.FindAsync(id);
            if (expenseGroup == null)
            {
                return NotFound();
            }

            _context.Expense_groups.Remove(expenseGroup);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExpenseGroupExists(int id)
        {
            return _context.Expense_groups.Any(e => e.Id == id);
        }
    }
}
