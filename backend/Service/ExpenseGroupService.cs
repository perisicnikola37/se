using Domain.Exceptions;
using Domain.Models;
using FluentValidation;
using Infrastructure.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Service;

public class ExpenseGroupService(DatabaseContext _context, IValidator<ExpenseGroup> _validator, GetAuthenticatedUserIdService getAuthenticatedUserIdService, ILogger<ExpenseGroupService> _logger)
{
	private readonly DatabaseContext _context = _context;
	private readonly IValidator<ExpenseGroup> _validator = _validator;
	private readonly ILogger<ExpenseGroupService> _logger = _logger;
	private readonly GetAuthenticatedUserIdService getAuthenticatedUserIdService = getAuthenticatedUserIdService;

	[HttpGet]
	public async Task<IEnumerable<object>> GetExpenseGroupsAsync()
	{
		try
		{
			var expenseGroups = await _context.ExpenseGroups
				.Include(e => e.Expenses)
					.ThenInclude(expense => expense.User)
				.OrderByDescending(e => e.CreatedAt)
				.ToListAsync();

			if (expenseGroups.Count != 0)
			{
				var simplifiedExpenseGroups = expenseGroups.Select(expenseGroup => new
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
				});

				return simplifiedExpenseGroups;
			}

			return expenseGroups;
		}
		catch (Exception ex)
		{
			_logger.LogError($"GetExpenseGroupsAsync: An error occurred. Error: {ex.Message}");
			throw;
		}
	}
	public async Task<ActionResult<ExpenseGroup>> GetExpenseGroupAsync(int id)
	{
		try
		{
			var expenseGroup = await _context.ExpenseGroups
				.Include(e => e.Expenses)
					.ThenInclude(expense => expense.User)
				.FirstOrDefaultAsync(e => e.Id == id);

			if (expenseGroup == null) return new NotFoundResult();

			var simplifiedExpenseGroup = new
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

			return new OkObjectResult(simplifiedExpenseGroup);
		}
		catch (Exception ex)
		{
			_logger.LogError($"GetExpenseGroupAsync: An error occurred. Error: {ex.Message}");
			throw;
		}
	}

	public async Task<ActionResult<ExpenseGroup>> CreateExpenseGroupAsync(ExpenseGroup expenseGroup, ControllerBase controller)
	{
		try
		{
			var validationResult = await _validator.ValidateAsync(expenseGroup);
			if (!validationResult.IsValid) return new BadRequestObjectResult(validationResult.Errors);
			
			_context.ExpenseGroups.Add(expenseGroup);
			await _context.SaveChangesAsync();

			return controller.CreatedAtAction("GetExpenseGroup", new { id = expenseGroup.Id }, expenseGroup);
		}
		catch (Exception ex)
		{
			_logger.LogError($"CreateExpenseGroupAsync: An error occurred. Error: {ex.Message}");
			throw;
		}
	}
	public async Task<IActionResult> UpdateExpenseGroupAsync(int id, ExpenseGroup expenseGroup)
	{
		try
		{
			if (id != expenseGroup.Id) return new BadRequestResult();

			_context.Entry(expenseGroup).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (ConflictException)
			{
				if (!ExpenseGroupExists(id)) return new NotFoundResult();

				throw new ConflictException("ExpenseGroupService.cs");
			}
			return new NoContentResult();
		}
		catch (Exception ex)
		{
			_logger.LogError($"UpdateExpenseGroupAsync: An error occurred. Error: {ex.Message}");
			throw;
		}
	}

	public async Task<IActionResult> DeleteExpenseGroupByIdAsync(int id)
	{
		try
		{
			var expenseGroup = await _context.ExpenseGroups.FindAsync(id);

			if (expenseGroup == null) return new NotFoundResult();

			_context.ExpenseGroups.Remove(expenseGroup);
			await _context.SaveChangesAsync();

			return new NoContentResult();
		}
		catch (Exception ex)
		{
			_logger.LogError($"DeleteExpenseGroupByIdAsync: An error occurred. Error: {ex.Message}");
			throw;
		}
	}

	private bool ExpenseGroupExists(int id)
	{
		return _context.ExpenseGroups.Any(e => e.Id == id);
	}
}