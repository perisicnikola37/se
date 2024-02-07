using System.Linq.Expressions;
using Contracts.Dto;
using Contracts.Filter;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using FluentValidation;
using Infrastructure.Contexts;
using Microsoft.AspNetCore.Http;
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
	public async Task<PagedResponseDto<List<ExpenseResponseDto>>> GetExpensesAsync(PaginationFilterDto filter,
		IHttpContextAccessor httpContextAccessor)
	{
		try
		{
			var authenticatedUserId = getAuthenticatedUserId.GetUserId(httpContextAccessor.HttpContext.User);

			string description = httpContextAccessor.HttpContext.Request.Query["description"]!;
			string minAmount = httpContextAccessor.HttpContext.Request.Query["minAmount"]!;
			string maxAmount = httpContextAccessor.HttpContext.Request.Query["maxAmount"]!;
			string expenseGroupId = httpContextAccessor.HttpContext.Request.Query["expenseGroupId"]!;

			var validFilter = new PaginationFilterDto(filter.PageNumber, filter.PageSize);

			var query = context.Expenses
				  .Where(e => e.UserId == authenticatedUserId)
				  .ApplyFilter(e => e.Description.Contains(description), !string.IsNullOrWhiteSpace(description))
				  .ApplyFilter(e => e.Amount >= float.Parse(minAmount), !string.IsNullOrWhiteSpace(minAmount))
				  .ApplyFilter(e => e.Amount <= float.Parse(maxAmount), !string.IsNullOrWhiteSpace(maxAmount))
				  .ApplyFilter(e => e.ExpenseGroupId == int.Parse(expenseGroupId), !string.IsNullOrWhiteSpace(expenseGroupId));

			var totalRecords = await query.CountAsync();
			var totalPages = (int)Math.Ceiling((double)totalRecords / validFilter.PageSize);

			var pagedData = await query
			   .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
			   .Take(validFilter.PageSize)
			   .Select(e => new ExpenseResponseDto
			   {
				   Id = e.Id,
				   Description = e.Description,
				   Amount = e.Amount,
				   CreatedAt = e.CreatedAt,
				   ExpenseGroupId = e.ExpenseGroupId,
				   ExpenseGroup = (e
					   .ExpenseGroup != null
					   ? new ExpenseGroupDto
					   {
						   Id = e.ExpenseGroup.Id,
						   Name = e.ExpenseGroup.Name,
						   Description = e.ExpenseGroup.Description
					   }
					   : null)!,
				   UserId = e.UserId
			   })
			   .OrderByDescending(e => e.CreatedAt)
			   .ToListAsync();


			var baseUri = new Uri(httpContextAccessor.HttpContext.Request.Scheme + "://" + httpContextAccessor.HttpContext.Request.Host.Value);
			var currentPageUri = new Uri(httpContextAccessor.HttpContext.Request.Path, UriKind.Relative);
			var nextPageUri = new Uri(baseUri,
				$"{currentPageUri}?pageNumber={validFilter.PageNumber + 1}&pageSize={validFilter.PageSize}");
			var previousPageUri = new Uri(baseUri,
				$"{currentPageUri}?pageNumber={validFilter.PageNumber - 1}&pageSize={validFilter.PageSize}");

			return new PagedResponseDto<List<ExpenseResponseDto>>(pagedData, validFilter.PageNumber, validFilter.PageSize)
			{
				PageNumber = validFilter.PageNumber,
				PageSize = validFilter.PageSize,
				FirstPage = new Uri(baseUri, $"{currentPageUri}?pageNumber=1&pageSize={validFilter.PageSize}"),
				LastPage =
					new Uri(baseUri, $"{currentPageUri}?pageNumber={totalPages}&pageSize={validFilter.PageSize}"),
				TotalPages = totalPages,
				TotalRecords = totalRecords,
				NextPage = validFilter.PageNumber < totalPages ? nextPageUri : null,
				PreviousPage = validFilter.PageNumber > 1 ? previousPageUri : null
			};
		}
		catch (Exception ex)
		{
			logger.LogError($"GetExpensesAsync: An error occurred. Error: {ex.Message}");
			throw;
		}
	}

	public async Task<object> GetLatestExpensesAsync(ControllerBase controller)
	{
		try
		{
			var authenticatedUserId = getAuthenticatedUserId.GetUserId(controller.User);

			var highestExpense = await context.Expenses
				.Where(i => i.CreatedAt >= DateTime.UtcNow.AddDays(-7) && i.UserId == authenticatedUserId)
				.OrderByDescending(i => i.Amount)
				.Select(i => i.Amount)
				.FirstOrDefaultAsync();

			var latestExpenses = await context.Expenses
				.Where(i => i.UserId == authenticatedUserId)
				.Include(e => e.ExpenseGroup)
				.OrderByDescending(e => e.CreatedAt)
				.Take(5)
				.ToListAsync();


			var response = new
			{
				highestExpense,
				expenses = latestExpenses
			};

			return response;
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

			if (expense == null) return null!;

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

	public async Task<IActionResult> DeleteAllExpensesAsync(ControllerBase controller)
	{
		try
		{
			var authenticatedUserId = getAuthenticatedUserId.GetUserId(controller.User);

			if (!authenticatedUserId.HasValue) return new BadRequestResult();

			var expensesToDelete = await context.Expenses
				.Where(e => e.UserId == authenticatedUserId.Value)
				.ToListAsync();

			if (expensesToDelete.Count == 0) return new NotFoundResult();

			context.Expenses.RemoveRange(expensesToDelete);
			await context.SaveChangesAsync();

			return new NoContentResult();
		}
		catch (Exception ex)
		{
			logger.LogError($"DeleteAllExpensesAsync: An error occurred. Error: {ex.Message}");
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
}