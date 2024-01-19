using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vega.Classes;
using Vega.Models;

namespace Vega.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class IncomeGroupController : ControllerBase
	{
		private readonly MainDatabaseContext _context;

		public IncomeGroupController(MainDatabaseContext context)
		{
			_context = context;
		}

		// GET: api/IncomeGroup
		[HttpGet]
		public async Task<ActionResult<IEnumerable<IncomeGroup>>> GetIncome_groups()
		{
			var income_groups = await _context.Income_groups
				.Include(e => e.Incomes)
				.OrderByDescending(e => e.Created_at)
				.ToListAsync();

			if (income_groups.Count != 0)
			{
				return income_groups;
			}
			else
			{
				return NotFound();
			}
		}

		// GET: api/IncomeGroup/5
		[HttpGet("{id}")]
		public async Task<ActionResult<IncomeGroup>> GetIncomeGroup(int id)
		{
			var incomeGroup = await _context.Income_groups
				.Include(e => e.Incomes)
				.FirstOrDefaultAsync(e => e.Id == id);


			if (incomeGroup == null)
			{
				return NotFound();
			}

			return incomeGroup;
		}

		// PUT: api/IncomeGroup/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutIncomeGroup(int id, IncomeGroup incomeGroup)
		{
			if (id != incomeGroup.Id)
			{
				return BadRequest();
			}

			_context.Entry(incomeGroup).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!IncomeGroupExists(id))
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

		// POST: api/IncomeGroup
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<IncomeGroup>> PostIncomeGroup(IncomeGroup incomeGroup)
		{
			_context.Income_groups.Add(incomeGroup);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetIncomeGroup", new { id = incomeGroup.Id }, incomeGroup);
		}

		// DELETE: api/IncomeGroup/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteIncomeGroup(int id)
		{
			var incomeGroup = await _context.Income_groups.FindAsync(id);
			if (incomeGroup == null)
			{
				return NotFound();
			}

			_context.Income_groups.Remove(incomeGroup);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool IncomeGroupExists(int id)
		{
			return _context.Income_groups.Any(e => e.Id == id);
		}
	}
}
