using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vega.Classes;
using Vega.Exceptions;
using Vega.Models;

namespace Vega.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ExpenseController : ControllerBase
	{
		private readonly MainDatabaseContext _context;
		private readonly GetAuthenticatedUserIdService _getAuthenticatedUserIdService;

		public ExpenseController(MainDatabaseContext context, GetAuthenticatedUserIdService getAuthenticatedUserIdService)
		{
			_context = context;
			_getAuthenticatedUserIdService = getAuthenticatedUserIdService;
		}

		// GET: api/Expense
		[HttpGet]
		public async Task<IActionResult> GetExpenses([FromQuery] PaginationFilter filter)
		{
			var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
			var pagedData = await _context.Expenses
				.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
				.Take(validFilter.PageSize)
				.ToListAsync();
			// var totalRecords = await _context.Expenses.CountAsync();
			return Ok(new PagedResponse<List<Expense>>(pagedData, validFilter.PageNumber, validFilter.PageSize));
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

		// GET: api/Expense/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Expense>> GetExpense(int id)
		{
			// TRYING CUSTOM TEMPLATE 
			// var expense = await _context.Expenses.FindAsync(id);

			var expense = await _context.Expenses.FindAsync(id);
			return Ok(new Response<Expense>(expense));


			// if (expense == null)
			// {
			//     return NotFound();
			// }

			// return expense;
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
			var expense_group = await _context.Expense_groups.FindAsync(expense.ExpenseGroupId);

			if (expense_group == null)
			{
				throw NotFoundException.Create("ExpenseGroupId", "Expense group not found.");
			}
			var userId = _getAuthenticatedUserIdService.GetUserId(User);
			expense.UserId = (int)userId;
			
			_context.Expenses.Add(expense);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetExpense", new { id = expense.Id }, expense);
		}

		// DELETE: api/Expense/5
		// [Authorize(Roles = "admin")]
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
