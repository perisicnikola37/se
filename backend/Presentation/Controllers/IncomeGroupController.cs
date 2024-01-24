using Domain.Models;
using Infrastructure.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IncomeGroupController(MainDatabaseContext context) : ControllerBase
{
    // GET: api/IncomeGroup
    [HttpGet]
    public async Task<ActionResult<IEnumerable<IncomeGroup>>> GetIncome_groups()
    {
        var incomeGroups = await context.IncomeGroups
            .Include(e => e.Incomes)
            .OrderByDescending(e => e.CreatedAt)
            .ToListAsync();

        if (incomeGroups.Count != 0)
            return incomeGroups;
        return NotFound();
    }

    // GET: api/IncomeGroup/5
    [HttpGet("{id}")]
    public async Task<ActionResult<IncomeGroup>> GetIncomeGroup(int id)
    {
        try
        {
            var incomeGroup = await context.IncomeGroups
                .Include(e => e.Incomes)!
                .ThenInclude(income => income.User)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (incomeGroup == null) return NotFound();

            var simplifiedIncomeGroup = new
            {
                incomeGroup.Id,
                incomeGroup.Name,
                incomeGroup.Description,
                Incomes = incomeGroup.Incomes?.Select(income => new
                {
                    income.Id,
                    income.Description,
                    income.Amount,
                    income.CreatedAt,
                    income.IncomeGroupId,
                    UserId = income.User?.Id,
                    UserUsername = income.User?.Username
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

    // PUT: api/IncomeGroup/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutIncomeGroup(int id, IncomeGroup incomeGroup)
    {
        if (id != incomeGroup.Id) return BadRequest();

        context.Entry(incomeGroup).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!IncomeGroupExists(id))
                return NotFound();
            throw;
        }

        return NoContent();
    }

    // POST: api/IncomeGroup
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<IncomeGroup>> PostIncomeGroup(IncomeGroup incomeGroup)
    {
        context.IncomeGroups.Add(incomeGroup);
        await context.SaveChangesAsync();

        return CreatedAtAction("GetIncomeGroup", new { id = incomeGroup.Id }, incomeGroup);
    }

    // DELETE: api/IncomeGroup/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteIncomeGroup(int id)
    {
        var incomeGroup = await context.IncomeGroups.FindAsync(id);
        if (incomeGroup == null) return NotFound();

        context.IncomeGroups.Remove(incomeGroup);
        await context.SaveChangesAsync();

        return NoContent();
    }

    private bool IncomeGroupExists(int id)
    {
        return context.IncomeGroups.Any(e => e.Id == id);
    }
}