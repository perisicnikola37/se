using Domain.Models;
using Infrastructure.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FluentValidation;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExpenseGroupController(MainDatabaseContext context, IValidator<ExpenseGroup> validator) : ControllerBase
{
	// GET: api/ExpenseGroup
	[HttpGet]
	public async Task<ActionResult<IEnumerable<ExpenseGroup>>> GetExpense_groups()
	{
		var expenseGroups = await context.ExpenseGroups
			.Include(e => e.Expenses)
			.OrderByDescending(e => e.CreatedAt)
			.ToListAsync();

		if (expenseGroups.Count != 0)
			return expenseGroups;
		return NotFound();
	}

	// GET: api/ExpenseGroup/5
	[HttpGet("{id}")]
	public async Task<ActionResult<ExpenseGroup>> GetExpenseGroup(int id)
	{
		// move this to a repository layer
		try
		{
			var expenseGroup = await context.ExpenseGroups
				.Include(e => e.Expenses)!
				.ThenInclude(expense => expense.User)
				.FirstOrDefaultAsync(e => e.Id == id);

			if (expenseGroup == null) return NotFound();

			var simplifiedIncomeGroup = new
			{
				expenseGroup.Id,
				expenseGroup.Name,
				expenseGroup.Description,
				Expenses = expenseGroup.Expenses?.Select(expense => new
				{
					expense.Id,
					expense.Description,
					expense.Amount,
					expense.CreatedAt,
					expense.ExpenseGroupId,
					UserId = expense.User?.Id,
					UserUsername = expense.User?.Username
				})
			};

			return Ok(simplifiedIncomeGroup);
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}
	}

	// PUT: api/ExpenseGroup/5
	// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
	[HttpPut("{id}")]
	public async Task<IActionResult> PutExpenseGroup(int id, ExpenseGroup expenseGroup)
	{
		if (id != expenseGroup.Id) return BadRequest();

		context.Entry(expenseGroup).State = EntityState.Modified;

		try
		{
			await context.SaveChangesAsync();
		}
		catch (DbUpdateConcurrencyException)
		{
			if (!ExpenseGroupExists(id))
				return NotFound();
			throw;
		}

		return NoContent();
	}

	// POST: api/ExpenseGroup
	// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
	[HttpPost]
	public async Task<ActionResult<ExpenseGroup>> PostExpenseGroup(ExpenseGroup expenseGroup)
	{
		var validationResult = await validator.ValidateAsync(expenseGroup);
		if (!validationResult.IsValid)
		{
			return BadRequest(validationResult.Errors);
		}
		
		context.ExpenseGroups.Add(expenseGroup);
		await context.SaveChangesAsync();

		return CreatedAtAction("GetExpenseGroup", new { id = expenseGroup.Id }, expenseGroup);
	}

	// DELETE: api/ExpenseGroup/5
	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteExpenseGroup(int id)
	{
		var expenseGroup = await context.ExpenseGroups.FindAsync(id);
		if (expenseGroup == null) return NotFound();

		context.ExpenseGroups.Remove(expenseGroup);
		await context.SaveChangesAsync();

		return NoContent();
	}

	private bool ExpenseGroupExists(int id)
	{
		return context.ExpenseGroups.Any(e => e.Id == id);
	}
}