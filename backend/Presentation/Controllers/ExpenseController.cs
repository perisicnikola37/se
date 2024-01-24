using Contracts.Dto;
using Contracts.Filter;
using Domain.Exceptions;
using Domain.Models;
using Infrastructure.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExpenseController(MainDatabaseContext context, GetAuthenticatedUserIdService getAuthenticatedUserIdService)
    : ControllerBase
{
    // GET: api/Expense
    [HttpGet]
    public async Task<IActionResult> GetExpenses([FromQuery] PaginationFilter filter)
    {
        var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
        var pagedData = await context.Expenses
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
        return await context.Expenses
            .Include(e => e.User)
            .OrderByDescending(e => e.CreatedAt)
            .Take(5)
            .ToListAsync();
    }

    // GET: api/Expense/total-amount
    [HttpGet("total-amount")]
    public async Task<ActionResult<int>> GetTotalAmountOfExpenses()
    {
        return await context.Expenses.CountAsync();
    }

    // GET: api/Expense/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Expense>> GetExpense(int id)
    {
        // TRYING CUSTOM TEMPLATE 
        // var expense = await _context.Expenses.FindAsync(id);
        var expense = await context.Expenses.FindAsync(id);
        return Ok(new Response<Expense>(expense!));

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
        if (id != expense.Id) return BadRequest();

        context.Entry(expense).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ExpenseExists(id))
                return NotFound();
            throw;
        }

        return NoContent();
    }

    // POST: api/Expense
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Expense>> PostExpense(Expense expense)
    {
        var expenseGroup = await context.ExpenseGroups.FindAsync(expense.ExpenseGroupId);

        if (expenseGroup == null) throw NotFoundException.Create("ExpenseGroupId", "Expense group not found.");
        var userId = getAuthenticatedUserIdService.GetUserId(User);
        expense.UserId = (int)userId!;

        context.Expenses.Add(expense);
        await context.SaveChangesAsync();

        return CreatedAtAction("GetExpense", new { id = expense.Id }, expense);
    }

    // DELETE: api/Expense/5
    // [Authorize(Roles = "admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteExpense(int id)
    {
        var expense = await context.Expenses.FindAsync(id);
        if (expense == null) return NotFound();

        context.Expenses.Remove(expense);
        await context.SaveChangesAsync();

        return NoContent();
    }

    private bool ExpenseExists(int id)
    {
        return context.Expenses.Any(e => e.Id == id);
    }
}