using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vega.Classes;
using Vega.Exceptions;
using Vega.Models;

namespace Vega.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncomeController : ControllerBase
    {
        private readonly MainDatabaseContext _context;

        public IncomeController(MainDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Income
        [HttpGet]
         public async Task<IActionResult> GetIncomes([FromQuery] PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = await _context.Incomes
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();
            // var totalRecords = await _context.Incomes.CountAsync();
            return Ok(new PagedResponse<List<Income>>(pagedData, validFilter.PageNumber, validFilter.PageSize));
        }

        // GET: api/Income/latest/5
        [HttpGet("latest/5")]
        public async Task<ActionResult<IEnumerable<Income>>> GetLatestIncomes()
        {
            var latestIncomes = await _context.Incomes
                                               .OrderByDescending(e => e.Created_at)
                                               .Take(5)
                                               .ToListAsync();

            return latestIncomes;
        }

        // GET: api/Income/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Income>> GetIncome(int id)
        {
            var income = await _context.Incomes.FindAsync(id);

            if (income == null)
            {
                return NotFound();
            }

            return income;
        }

        // PUT: api/Income/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIncome(int id, Income income)
        {
            if (id != income.Id)
            {
                return BadRequest();
            }

            _context.Entry(income).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IncomeExists(id))
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

        // POST: api/Income
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Income>> PostIncome(Income income)
        {
            var income_group = await _context.Income_groups.FindAsync(income.IncomeGroupId);

            if (income_group == null)
            {
               throw NotFoundException.Create("IncomeGroupId", "Income group not found.");
            }

            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "Id");
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                return Unauthorized(new { message = "Invalid user claims" });
            }
            income.UserId = userId;
            _context.Incomes.Add(income);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIncome", new { id = income.Id }, income);
        }

        // DELETE: api/Income/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIncome(int id)
        {
            var income = await _context.Incomes.FindAsync(id);
            if (income == null)
            {
                return NotFound();
            }

            _context.Incomes.Remove(income);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IncomeExists(int id)
        {
            return _context.Incomes.Any(e => e.Id == id);
        }
    }
}
