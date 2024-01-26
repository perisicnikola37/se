using System.Linq.Expressions;
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

public class ExpenseService(DatabaseContext _context, IValidator<Expense> _validator, GetAuthenticatedUserIdService _getAuthenticatedUserIdService, ILogger<ExpenseService> _logger)
{
	private readonly DatabaseContext _context = _context;
	private readonly IValidator<Expense> _validator = _validator;
	private readonly ILogger<ExpenseService> _logger = _logger;
	private readonly GetAuthenticatedUserIdService getAuthenticatedUserIdService = _getAuthenticatedUserIdService;

	[HttpGet]
	public async Task<PagedResponse<List<ExpenseResponse>>> GetExpensesAsync(
	PaginationFilter filter,
	ControllerBase controller)
	{
		try
		{
			int? authenticatedUserId = _getAuthenticatedUserIdService.GetUserId(controller.User);

			string description = controller.HttpContext.Request.Query["description"];
			string minPrice = controller.HttpContext.Request.Query["minPrice"];
			string maxPrice = controller.HttpContext.Request.Query["maxPrice"];
			string expenseGroupId = controller.HttpContext.Request.Query["expenseGroupId"];

			var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);

			var query = _context.Expenses
				.Include(e => e.User)
				.Where(e => e.UserId == authenticatedUserId);

			query = ApplyFilter(query, e => e.Description.Contains(description), !string.IsNullOrWhiteSpace(description));
			query = ApplyFilter(query, e => e.Amount >= float.Parse(minPrice), !string.IsNullOrWhiteSpace(minPrice));
			query = ApplyFilter(query, e => e.Amount <= float.Parse(maxPrice), !string.IsNullOrWhiteSpace(maxPrice));
			query = ApplyFilter(query, e => e.ExpenseGroupId == int.Parse(expenseGroupId), !string.IsNullOrWhiteSpace(expenseGroupId));

			var pagedData = await query
				.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
				.Take(validFilter.PageSize)
				.Select(e => new ExpenseResponse
				{
					Id = e.Id,
					Description = e.Description,
					Amount = e.Amount,
					CreatedAt = e.CreatedAt,
					ExpenseGroupId = e.ExpenseGroupId,
					ExpenseGroup = e.ExpenseGroup,
					UserId = e.UserId,
					User = new UserResponse
					{
						Username = e.User.Username
					}
				})
				.ToListAsync();

			return new PagedResponse<List<ExpenseResponse>>(pagedData, validFilter.PageNumber, validFilter.PageSize);
		}
		catch (Exception ex)
		{
			_logger.LogError($"GetExpensesAsync: An error occurred. Error: {ex.Message}");
			throw;
		}
	}

	public async Task<List<Expense>> GetLatestExpensesAsync()
	{
		try
		{
			return await _context.Expenses
				.Include(e => e.User)
				.Include(e => e.ExpenseGroup)
				.OrderByDescending(e => e.CreatedAt)
				.Take(5)
				.ToListAsync();
		}
		catch (Exception ex)
		{
			_logger.LogError($"GetLatestExpensesAsync: An error occurred. Error: {ex.Message}");
			throw;
		}
	}

	public async Task<Response<Expense>?> GetExpenseAsync(int id)
	{
		try
		{
			var expense = await _context.Expenses
				.Where(e => e.Id == id)
				.FirstOrDefaultAsync();

			if (expense == null) return null;

			return new Response<Expense>(expense);
		}
		catch (Exception ex)
		{
			_logger.LogError($"GetExpenseAsync: An error occurred. Error: {ex.Message}");
			throw;
		}
	}

	public async Task<ActionResult<Expense>> CreateExpenseAsync(Expense expense, ControllerBase controller)
	{
		try
		{
			var validationResult = await _validator.ValidateAsync(expense);
			if (!validationResult.IsValid) return new BadRequestObjectResult(validationResult.Errors);

			var expenseGroup = await _context.ExpenseGroups.FindAsync(expense.ExpenseGroupId) ?? throw NotFoundException.Create("ExpenseGroupId", "Expense group not found.");

			var userId = getAuthenticatedUserIdService.GetUserId(controller.User);

			expense.UserId = (int)userId!;

			_context.Expenses.Add(expense);

			await _context.SaveChangesAsync();

			return new CreatedAtActionResult("GetExpense", "Expense", new { id = expense.Id }, expense);
		}
		catch (Exception ex)
		{
			_logger.LogError($"CreateExpenseAsync: An error occurred. Error: {ex.Message}");
			throw;
		}
	}
	public async Task<IActionResult> UpdateExpenseAsync(int id, Expense updatedExpense, ControllerBase controller)
	{
		try
		{
			if (id != updatedExpense.Id) return new BadRequestResult();

			int? authenticatedUserId = _getAuthenticatedUserIdService.GetUserId(controller.User);

			// Check if authenticatedUserId has a value
			if (authenticatedUserId.HasValue)
			{
				// Attach authenticated user id
				updatedExpense.UserId = authenticatedUserId.Value;

				_context.Entry(updatedExpense).State = EntityState.Modified;

				try
				{
					await _context.SaveChangesAsync();
				}
				catch (ConflictException)
				{
					if (!ExpenseExists(id)) return new NotFoundResult();
					throw new ConflictException("ExpenseService.cs");
				}

				return new NoContentResult();
			}
			else
			{
				return new BadRequestResult();
			}
		}
		catch (Exception ex)
		{
			_logger.LogError($"UpdateExpenseAsync: An error occurred. Error: {ex.Message}");
			throw;
		}
	}

	public async Task<IActionResult> DeleteExpenseByIdAsync(int id)
	{
		try
		{
			var expense = await _context.Expenses.FindAsync(id);

			if (expense == null) return new NotFoundResult();

			_context.Expenses.Remove(expense);
			await _context.SaveChangesAsync();

			return new NoContentResult();
		}
		catch (Exception ex)
		{
			_logger.LogError($"DeleteExpenseByIdAsync: An error occurred. Error: {ex.Message}");
			throw;
		}
	}
	
	public async Task<ActionResult<int>> GetTotalAmountOfExpensesAsync()
	{
		try
		{
			return await _context.Expenses.CountAsync();
		}
		catch (Exception ex)
		{
			_logger.LogError($"GetTotalAmountOfExpensesAsync: An error occurred. Error: {ex.Message}");
			throw;
		}
	}

	private bool ExpenseExists(int id)
	{
		try
		{
			return _context.Expenses.Any(e => e.Id == id);
		}
		catch (Exception ex)
		{
			_logger.LogError($"ExpenseExists: An error occurred. Error: {ex.Message}");
			throw;
		}
	}

	static IQueryable<Expense> ApplyFilter(IQueryable<Expense> query, Expression<Func<Expense, bool>> filter, bool condition)
	{
		return condition ? query.Where(filter) : query;
	}
}
