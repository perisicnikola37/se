using Contracts.Dto;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using FluentValidation;
using Infrastructure.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Service;

public class IncomeGroupService(DatabaseContext context, IValidator<IncomeGroup> validator, ILogger<IncomeGroupService> logger, IGetAuthenticatedUserIdService getAuthenticatedUserId) : IIncomeGroupService
{
	[HttpGet]
	public async Task<IEnumerable<IncomeGroupDto>> GetIncomeGroupsAsync(ControllerBase controller)
	{
		try
		{
			var authenticatedUserId = getAuthenticatedUserId.GetUserId(controller.User);

			var incomeGroups = await context.IncomeGroups
			 	.Where(ig => ig.UserId == authenticatedUserId)
				.Include(e => e.Incomes!)
					.ThenInclude(income => income.User)
				.OrderByDescending(e => e.CreatedAt)
				.ToListAsync();

			if (incomeGroups.Count != 0)
			{
				var simplifiedIncomeGroups = incomeGroups.Select(incomeGroup => new IncomeGroupDto
				{
					Id = incomeGroup.Id,
					Name = incomeGroup.Name,
					Description = incomeGroup.Description,
					Incomes = incomeGroup.Incomes?.Select(expense => new IncomeDto
					{
						Id = expense.Id,
						Description = expense.Description,
						Amount = expense.Amount,
						CreatedAt = expense.CreatedAt,
						IncomeGroupId = expense.IncomeGroupId,
						UserId = expense.User?.Id,
						Username = expense.User?.Username
					})
				});

				return simplifiedIncomeGroups;
			}
			return [];
		}
		catch (Exception ex)
		{
			logger.LogError($"GetIncomeGroupsAsync: An error occurred. Error: {ex.Message}");
			throw;
		}
	}

	public async Task<ActionResult<IncomeGroup>> GetIncomeGroupAsync(int id)
	{
		try
		{
			var incomeGroup = await context.IncomeGroups
				.Include(e => e.Incomes)!
					.ThenInclude(income => income.User)
				.FirstOrDefaultAsync(e => e.Id == id);

			if (incomeGroup == null) return new NotFoundResult();

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

			return new OkObjectResult(simplifiedIncomeGroup);
		}
		catch (Exception ex)
		{
			logger.LogError($"GetIncomeGroupAsync: An error occurred. Error: {ex.Message}");
			throw;
		}
	}

	public async Task<ActionResult<IncomeGroup>> CreateIncomeGroupAsync(IncomeGroup incomeGroup, ControllerBase controller)
	{
		try
		{
			var validationResult = await validator.ValidateAsync(incomeGroup);
			if (!validationResult.IsValid) return new BadRequestObjectResult(validationResult.Errors);

			var userId = getAuthenticatedUserId.GetUserId(controller.User);

			incomeGroup.UserId = (int)userId!;

			context.IncomeGroups.Add(incomeGroup);
			await context.SaveChangesAsync();

			return controller.Ok(incomeGroup);
		}
		catch (Exception ex)
		{
			logger.LogError($"CreateIncomeGroupAsync: An error occurred. Error: {ex.Message}");
			throw;
		}
	}

	public async Task<IActionResult> UpdateIncomeGroupAsync(int id, IncomeGroup incomeGroup, ControllerBase controller)
	{
		try
		{
			if (id != incomeGroup.Id) return new BadRequestResult();

			var authenticatedUserId = getAuthenticatedUserId.GetUserId(controller.User);

			// Check if authenticatedUserId has a value
			if (authenticatedUserId.HasValue)
			{
				incomeGroup.UserId = authenticatedUserId.Value;

			}

			context.Entry(incomeGroup).State = EntityState.Modified;

			try
			{
				await context.SaveChangesAsync();
			}
			catch (ConflictException)
			{
				if (!IncomeGroupExists(id)) return new NotFoundResult();

				throw new ConflictException();
			}
			return new NoContentResult();
		}
		catch (Exception ex)
		{
			logger.LogError($"UpdateIncomeGroupAsync: An error occurred. Error: {ex.Message}");
			throw;
		}
	}

	public async Task<IActionResult> DeleteIncomeGroupByIdAsync(int id)
	{
		try
		{
			var incomeGroup = await context.IncomeGroups.FindAsync(id);

			if (incomeGroup == null) return new NotFoundResult();

			context.IncomeGroups.Remove(incomeGroup);
			await context.SaveChangesAsync();

			return new NoContentResult();
		}
		catch (Exception ex)
		{
			logger.LogError($"DeleteIncomeGroupByIdAsync: An error occurred. Error: {ex.Message}");
			throw;
		}
	}

	private bool IncomeGroupExists(int id)
	{
		return context.IncomeGroups.Any(e => e.Id == id);
	}
}