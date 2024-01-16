using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vega.classes;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly MyDBContext _context;

        public ExpenseController(MyDBContext context)
        {
            _context = context;
        }

        // GET: api/Expense
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Expense>>> GetExpenses()
        {
            var expenses = await _context.Expenses.OrderByDescending(e => e.Created_at).ToListAsync();

            if (expenses.Count != 0)
            {
                return expenses;
            }
            else
            {
                return NotFound();
            }
        }

        // GET: api/Expense/latest/5
        [HttpGet("latest/5")]
        public async Task<ActionResult<IEnumerable<Expense>>> GetLatestExpenses()
        {
            return await _context.Expenses
                                               .OrderByDescending(e => e.Created_at)
                                               .Take(5)
                                               .ToListAsync();

        }

        // GET: api/Expense/total-amount
        [HttpGet("total-amount")]
        public async Task<ActionResult<int>> GetTotalAmountOfExpenses()
        {
            return await _context.Expenses.CountAsync();
        }

        public class FinancialSummary
        {
            public int ExpenseCount { get; set; }
            public int IncomeCount { get; set; }
        }


        // GET: api/Expense/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Expense>> GetExpense(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);

            if (expense == null)
            {
                return NotFound();
            }

            return expense;
        }

        // PUT: api/Expense/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExpense(int id, Expense expense)
        {
            if (id != expense.Id)
            {
                return BadRequest();
            }

            _context.Entry(expense).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExpenseExists(id))
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

        // POST: api/Expense
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Expense>> PostExpense(Expense expense)
        {
            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExpense", new { id = expense.Id }, expense);
        }

        // DELETE: api/Expense/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            if (expense == null)
            {
                return NotFound();
            }

            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExpenseExists(int id)
        {
            return _context.Expenses.Any(e => e.Id == id);
        }
    }
}
