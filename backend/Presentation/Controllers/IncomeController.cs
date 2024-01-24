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
public class IncomeController(MainDatabaseContext context, GetAuthenticatedUserIdService getAuthenticatedUserIdService)
    : ControllerBase
{
    private readonly MainDatabaseContext _context = context;
    private readonly GetAuthenticatedUserIdService _getAuthenticatedUserIdService = getAuthenticatedUserIdService;

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
        return await _context.Incomes
            .Include(e => e.User)
            .OrderByDescending(e => e.CreatedAt)
            .Take(5)
            .ToListAsync();
    }

    // GET: api/Income/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Income>> GetIncome(int id)
    {
        var income = await _context.Incomes.FindAsync(id);

        if (income == null) return NotFound();

        return income;
    }

    // PUT: api/Income/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutIncome(int id, Income income)
    {
        if (id != income.Id) return BadRequest();

        _context.Entry(income).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!IncomeExists(id))
                return NotFound();
            throw;
        }

        return NoContent();
    }

    // POST: api/Income
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Income>> PostIncome(Income income)
    {
        var incomeGroup = await _context.IncomeGroups.FindAsync(income.IncomeGroupId);

        if (incomeGroup == null) throw NotFoundException.Create("IncomeGroupId", "Income group not found.");

        var userId = _getAuthenticatedUserIdService.GetUserId(User);
        income.UserId = (int)userId!;

        _context.Incomes.Add(income);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetIncome", new { id = income.Id }, income);
    }

    // DELETE: api/Income/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteIncome(int id)
    {
        var income = await _context.Incomes.FindAsync(id);
        if (income == null) return NotFound();

        _context.Incomes.Remove(income);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool IncomeExists(int id)
    {
        return _context.Incomes.Any(e => e.Id == id);
    }
}