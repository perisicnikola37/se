using Contracts.Dto;
using Contracts.Filter;
using Domain.Exceptions;
using Domain.Models;
using FluentValidation;
using Infrastructure.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Service;

public class ExpenseService
{
	private readonly DatabaseContext context;
	private readonly IValidator<Expense> validator;
	private readonly ILogger<ExpenseService> logger;
	private readonly GetAuthenticatedUserIdService getAuthenticatedUserIdService;

	public ExpenseService(DatabaseContext dbContext, IValidator<Expense> validator, GetAuthenticatedUserIdService getAuthenticatedUserIdService, ILogger<ExpenseService> logger)
	{
		context = dbContext;
		this.validator = validator;
		this.getAuthenticatedUserIdService = getAuthenticatedUserIdService;
		this.logger = logger;
	}

	[HttpGet]
	public async Task<PagedResponse<List<Expense>>> GetExpenses(PaginationFilter filter)
	{
		var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
		var pagedData = await context.Expenses
			.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
			.Take(validFilter.PageSize)
			.ToListAsync();

		return new PagedResponse<List<Expense>>(pagedData, validFilter.PageNumber, validFilter.PageSize);
	}

	public async Task<List<Expense>> GetLatestExpenses()
	{
		return await context.Expenses
			.Include(e => e.User)
			.Include(e => e.ExpenseGroup)
			.OrderByDescending(e => e.CreatedAt)
			.Take(5)
			.ToListAsync();
	}

	public async Task<Response<Expense>?> GetExpenseById(int id)
	{
		var expense = await context.Expenses
			.Where(e => e.Id == id)
			.FirstOrDefaultAsync();

		if (expense == null)
		{
			return null;
		}

		return new Response<Expense>(expense);
	}

	public async Task<ActionResult<Expense>> CreateExpenseAsync(Expense expense, ControllerBase controller)
	{
		var validationResult = await validator.ValidateAsync(expense);
		if (!validationResult.IsValid)
		{
			return new BadRequestObjectResult(validationResult.Errors);
		}

		var expenseGroup = await context.ExpenseGroups.FindAsync(expense.ExpenseGroupId) ?? throw NotFoundException.Create("ExpenseGroupId", "Expense group not found.");

		var userId = getAuthenticatedUserIdService.GetUserId(controller.User);

		expense.UserId = (int)userId!;

		context.Expenses.Add(expense);

		await context.SaveChangesAsync();

		return new CreatedAtActionResult("GetExpense", "Expense", new { id = expense.Id }, expense);
	}

	public async Task<IActionResult> UpdateExpense(int id, Expense updatedExpense)
	{
		if (id != updatedExpense.Id)
		{
			return new BadRequestResult();
		}

		context.Entry(updatedExpense).State = EntityState.Modified;

		try
		{
			await context.SaveChangesAsync();
		}
		catch (DbUpdateConcurrencyException)
		{
			if (!ExpenseExists(id))
			{
				return new NotFoundResult();
			}
			throw;
		}

		return new NoContentResult();
	}

	public async Task<IActionResult> DeleteExpenseById(int id)
	{
		var expense = await context.Expenses.FindAsync(id);

		if (expense == null)
		{
			return new NotFoundResult();
		}

		context.Expenses.Remove(expense);
		await context.SaveChangesAsync();

		return new NoContentResult();
	}

	public async Task<ActionResult<int>> GetTotalAmountOfExpenses()
	{
		return await context.Expenses.CountAsync();
	}

	private bool ExpenseExists(int id)
	{
		return context.Expenses.Any(e => e.Id == id);
	}
}
