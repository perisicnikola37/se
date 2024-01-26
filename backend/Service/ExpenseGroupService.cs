using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using FluentValidation;
using Infrastructure.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Service;

public class ExpenseGroupService(DatabaseContext context, IValidator<ExpenseGroup> validator,ILogger<ExpenseGroupService> logger): IExpenseGroupService
{
	[HttpGet]
	public async Task<IEnumerable<object>> GetExpenseGroupsAsync()
	{
		try
		{
			var expenseGroups = await context.ExpenseGroups
				.Include(e => e.Expenses)!
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
			logger.LogError($"GetExpenseGroupsAsync: An error occurred. Error: {ex.Message}");
			throw;
		}
	}
	public async Task<ActionResult<ExpenseGroup>> GetExpenseGroupAsync(int id)
	{
		try
		{
			var expenseGroup = await context.ExpenseGroups
				.Include(e => e.Expenses)!
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
			logger.LogError($"GetExpenseGroupAsync: An error occurred. Error: {ex.Message}");
			throw;
		}
	}

	public async Task<ActionResult<ExpenseGroup>> CreateExpenseGroupAsync(ExpenseGroup expenseGroup, ControllerBase controller)
	{
		try
		{
			var validationResult = await validator.ValidateAsync(expenseGroup);
			if (!validationResult.IsValid) return new BadRequestObjectResult(validationResult.Errors);
			
			context.ExpenseGroups.Add(expenseGroup);
			await context.SaveChangesAsync();

			return controller.CreatedAtAction("GetExpenseGroup", new { id = expenseGroup.Id }, expenseGroup);
		}
		catch (Exception ex)
		{
			logger.LogError($"CreateExpenseGroupAsync: An error occurred. Error: {ex.Message}");
			throw;
		}
	}
	public async Task<IActionResult> UpdateExpenseGroupAsync(int id, ExpenseGroup expenseGroup)
	{
		try
		{
			if (id != expenseGroup.Id) return new BadRequestResult();

			context.Entry(expenseGroup).State = EntityState.Modified;

			try
			{
				await context.SaveChangesAsync();
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
			logger.LogError($"UpdateExpenseGroupAsync: An error occurred. Error: {ex.Message}");
			throw;
		}
	}

	public async Task<IActionResult> DeleteExpenseGroupByIdAsync(int id)
	{
		try
		{
			var expenseGroup = await context.ExpenseGroups.FindAsync(id);

			if (expenseGroup == null) return new NotFoundResult();

			context.ExpenseGroups.Remove(expenseGroup);
			await context.SaveChangesAsync();

			return new NoContentResult();
		}
		catch (Exception ex)
		{
			logger.LogError($"DeleteExpenseGroupByIdAsync: An error occurred. Error: {ex.Message}");
			throw;
		}
	}

	private bool ExpenseGroupExists(int id)
	{
		return context.ExpenseGroups.Any(e => e.Id == id);
	}
}