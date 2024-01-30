using System.Linq.Expressions;
using Contracts.Dto;
using Contracts.Filter;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using FluentValidation;
using Infrastructure.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Service;

public class ExpenseService(
	DatabaseContext context,
	IValidator<Expense> validator,
	IGetAuthenticatedUserIdService getAuthenticatedUserId,
	ILogger<ExpenseService> logger) : IExpenseService
{
	[HttpGet]
	public async Task<PagedResponseDto<List<ExpenseResponse>>> GetExpensesAsync(
		PaginationFilterDto filter,
		ControllerBase controller)
	{
		try
		{
			var authenticatedUserId = getAuthenticatedUserId.GetUserId(controller.User);

			string description = controller.HttpContext.Request.Query["description"]!;
			string minPrice = controller.HttpContext.Request.Query["minPrice"]!;
			string maxPrice = controller.HttpContext.Request.Query["maxPrice"]!;
			string expenseGroupId = controller.HttpContext.Request.Query["expenseGroupId"]!;

			var validFilter = new PaginationFilterDto(filter.PageNumber, filter.PageSize);

			var query = context.Expenses
				.Include(e => e.User)
				.Where(e => e.UserId == authenticatedUserId);

			query = ApplyFilter(query, e => e.Description.Contains(description),
				!string.IsNullOrWhiteSpace(description));
			query = ApplyFilter(query, e => e.Amount >= float.Parse(minPrice), !string.IsNullOrWhiteSpace(minPrice));
			query = ApplyFilter(query, e => e.Amount <= float.Parse(maxPrice), !string.IsNullOrWhiteSpace(maxPrice));
			query = ApplyFilter(query, e => e.ExpenseGroupId == int.Parse(expenseGroupId),
				!string.IsNullOrWhiteSpace(expenseGroupId));

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
					ExpenseGroup = e.ExpenseGroup!,
					UserId = e.UserId,
					User = new UserResponse
					{
						Username = e.User!.Username
					}
				})
				.OrderByDescending(e => e.CreatedAt)
				.ToListAsync();

			return new PagedResponseDto<List<ExpenseResponse>>(pagedData, validFilter.PageNumber, validFilter.PageSize);
		}
		catch (Exception ex)
		{
			logger.LogError($"GetExpensesAsync: An error occurred. Error: {ex.Message}");
			throw;
		}
	}

	public async Task<List<Expense>> GetLatestExpensesAsync(ControllerBase controller)
	{
		try
		{
			var authenticatedUserId = getAuthenticatedUserId.GetUserId(controller.User);

			return await context.Expenses
				.Include(e => e.ExpenseGroup)
				.OrderByDescending(e => e.CreatedAt)
				.Take(5)
				.ToListAsync();
		}
		catch (Exception ex)
		{
			logger.LogError($"GetLatestExpensesAsync: An error occurred. Error: {ex.Message}");
			throw;
		}
	}

	public async Task<ActionResult<Expense>> GetExpenseAsync(int id)
	{
		try
		{
			var expense = await context.Expenses
				.Where(e => e.Id == id)
				.FirstOrDefaultAsync();

			if (expense == null) return null;

			return new OkObjectResult(expense);
		}
		catch (Exception ex)
		{
			logger.LogError($"GetExpenseAsync: An error occurred. Error: {ex.Message}");
			throw;
		}
	}

	public async Task<ActionResult<Expense>> CreateExpenseAsync(Expense expense, ControllerBase controller)
	{
		try
		{
			var validationResult = await validator.ValidateAsync(expense);
			if (!validationResult.IsValid) return new BadRequestObjectResult(validationResult.Errors);

			_ = await context.ExpenseGroups.FindAsync(expense.ExpenseGroupId) ??
				throw NotFoundException.Create("ExpenseGroupId", "Expense group not found.");

			var userId = getAuthenticatedUserId.GetUserId(controller.User);

			expense.UserId = (int)userId!;

			context.Expenses.Add(expense);

			await context.SaveChangesAsync();

			return new CreatedAtActionResult("GetExpense", "Expense", new { id = expense.Id }, expense);
		}
		catch (Exception ex)
		{
			logger.LogError($"CreateExpenseAsync: An error occurred. Error: {ex.Message}");
			throw;
		}
	}

	public async Task<IActionResult> UpdateExpenseAsync(int id, Expense updatedExpense, ControllerBase controller)
	{
		try
		{
			if (id != updatedExpense.Id) return new BadRequestResult();

			var authenticatedUserId = getAuthenticatedUserId.GetUserId(controller.User);

			// Check if authenticatedUserId has a value
			if (authenticatedUserId.HasValue)
			{
				// Attach authenticated user id
				updatedExpense.UserId = authenticatedUserId.Value;

				context.Entry(updatedExpense).State = EntityState.Modified;

				try
				{
					await context.SaveChangesAsync();
				}
				catch (ConflictException)
				{
					if (!ExpenseExists(id)) return new NotFoundResult();
					throw new ConflictException("ExpenseService.cs");
				}

				return new NoContentResult();
			}

			return new BadRequestResult();
		}
		catch (Exception ex)
		{
			logger.LogError($"UpdateExpenseAsync: An error occurred. Error: {ex.Message}");
			throw;
		}
	}

	public async Task<IActionResult> DeleteExpenseByIdAsync(int id)
	{
		try
		{
			var expense = await context.Expenses.FindAsync(id);

			if (expense == null) return new NotFoundResult();

			context.Expenses.Remove(expense);
			await context.SaveChangesAsync();

			return new NoContentResult();
		}
		catch (Exception ex)
		{
			logger.LogError($"DeleteExpenseByIdAsync: An error occurred. Error: {ex.Message}");
			throw;
		}
	}

	public async Task<ActionResult<int>> GetTotalAmountOfExpensesAsync()
	{
		try
		{
			return await context.Expenses.CountAsync();
		}
		catch (Exception ex)
		{
			logger.LogError($"GetTotalAmountOfExpensesAsync: An error occurred. Error: {ex.Message}");
			throw;
		}
	}

	private bool ExpenseExists(int id)
	{
		try
		{
			return context.Expenses.Any(e => e.Id == id);
		}
		catch (Exception ex)
		{
			logger.LogError($"ExpenseExists: An error occurred. Error: {ex.Message}");
			throw;
		}
	}

	private static IQueryable<Expense> ApplyFilter(IQueryable<Expense> query, Expression<Func<Expense, bool>> filter,
		bool condition)
	{
		return condition ? query.Where(filter) : query;
	}
}