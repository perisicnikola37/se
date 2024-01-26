using Domain.Exceptions;
using Domain.Models;
using FluentValidation;
using Infrastructure.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Service;

public class IncomeGroupService(DatabaseContext _context, IValidator<IncomeGroup> _validator, GetAuthenticatedUserIdService getAuthenticatedUserIdService, ILogger<IncomeGroupService> _logger)
{
	private readonly DatabaseContext _context = _context;
	private readonly IValidator<IncomeGroup> _validator = _validator;
	private readonly ILogger<IncomeGroupService> _logger = _logger;

	[HttpGet]
	public async Task<IEnumerable<object>> GetIncomeGroupsAsync()
	{
		try
		{
			var incomeGroups = await _context.IncomeGroups
				.Include(e => e.Incomes)
					.ThenInclude(income => income.User)
				.OrderByDescending(e => e.CreatedAt)
				.ToListAsync();

			if (incomeGroups.Count != 0)
			{
				var simplifiedIncomeGroups = incomeGroups.Select(incomeGroups => new
				{
					incomeGroups.Id,
					incomeGroups.Name,
					incomeGroups.Description,
					Incomes = incomeGroups.Incomes?.Select(income => new
					{
						income.Id,
						income.Description,
						income.Amount,
						income.CreatedAt,
						income.IncomeGroupId,
						UserId = income.User?.Id,
						UserUsername = income.User?.Username
					})
				});

				return simplifiedIncomeGroups;
			}

			return incomeGroups;
		}
		catch (Exception ex)
		{
			_logger.LogError($"GetIncomeGroupsAsync: An error occurred. Error: {ex.Message}");
			throw;
		}
	}
	public async Task<ActionResult<IncomeGroup>> GetIncomeGroupAsync(int id)
	{
		try
		{
			var incomeGroup = await _context.IncomeGroups
				.Include(e => e.Incomes)
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
			_logger.LogError($"GetIncomeGroupAsync: An error occurred. Error: {ex.Message}");
			throw;
		}
	}

	public async Task<ActionResult<IncomeGroup>> CreateIncomeGroupAsync(IncomeGroup incomeGroup, ControllerBase controller)
	{
		try
		{
			var validationResult = await _validator.ValidateAsync(incomeGroup);
			if (!validationResult.IsValid) return new BadRequestObjectResult(validationResult.Errors);
			
			_context.IncomeGroups.Add(incomeGroup);
			await _context.SaveChangesAsync();

			return controller.CreatedAtAction("GetIncomeGroup", new { id = incomeGroup.Id }, incomeGroup);
		}
		catch (Exception ex)
		{
			_logger.LogError($"CreateIncomeGroupAsync: An error occurred. Error: {ex.Message}");
			throw;
		}
	}
	public async Task<IActionResult> UpdateIncomeGroupAsync(int id, IncomeGroup incomeGroup)
	{
		try
		{
			if (id != incomeGroup.Id) return new BadRequestResult();

			_context.Entry(incomeGroup).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (ConflictException)
			{
				if (!IncomeGroupExists(id)) return new NotFoundResult();

				throw new ConflictException("IncomeGroupService.cs");
			}
			return new NoContentResult();
		}
		catch (Exception ex)
		{
			_logger.LogError($"UpdateIncomeGroupAsync: An error occurred. Error: {ex.Message}");
			throw;
		}
	}

	public async Task<IActionResult> DeleteIncomeGroupByIdAsync(int id)
	{
		try
		{
			var incomeGroup = await _context.IncomeGroups.FindAsync(id);

			if (incomeGroup == null) return new NotFoundResult();

			_context.IncomeGroups.Remove(incomeGroup);
			await _context.SaveChangesAsync();

			return new NoContentResult();
		}
		catch (Exception ex)
		{
			_logger.LogError($"DeleteIncomeGroupByIdAsync: An error occurred. Error: {ex.Message}");
			throw;
		}
	}

	private bool IncomeGroupExists(int id)
	{
		return _context.IncomeGroups.Any(e => e.Id == id);
	}
}