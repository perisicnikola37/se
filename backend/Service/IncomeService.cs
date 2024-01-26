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

public class IncomeService(
    DatabaseContext context,
    IValidator<Income> validator,
    IGetAuthenticatedUserIdService getAuthenticatedUserId,
    ILogger<IncomeService> logger) : IIncomeService
{
    [HttpGet]
    public async Task<PagedResponseDto<List<IncomeResponse>>> GetIncomesAsync(PaginationFilterDto filter,
        ControllerBase controller)
    {
        try
        {
            var authenticatedUserId = getAuthenticatedUserId.GetUserId(controller.User);

            string description = controller.HttpContext.Request.Query["description"]!;
            string minAmount = controller.HttpContext.Request.Query["minAmount"]!;
            string maxAmount = controller.HttpContext.Request.Query["maxAmount"]!;
            string incomeGroupId = controller.HttpContext.Request.Query["incomeGroupId"]!;

            var validFilter = new PaginationFilterDto(filter.PageNumber, filter.PageSize);

            var query = context.Incomes
                .Include(e => e.User)
                .Where(e => e.UserId == authenticatedUserId);

            query = ApplyFilter(query, e => e.Description.Contains(description),
                !string.IsNullOrWhiteSpace(description));
            query = ApplyFilter(query, e => e.Amount >= float.Parse(minAmount), !string.IsNullOrWhiteSpace(minAmount));
            query = ApplyFilter(query, e => e.Amount <= float.Parse(maxAmount), !string.IsNullOrWhiteSpace(maxAmount));
            query = ApplyFilter(query, e => e.IncomeGroupId == int.Parse(incomeGroupId),
                !string.IsNullOrWhiteSpace(incomeGroupId));

            var pagedData = await query
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .Select(e => new IncomeResponse
                {
                    Id = e.Id,
                    Description = e.Description,
                    Amount = e.Amount,
                    CreatedAt = e.CreatedAt,
                    IncomeGroupId = e.IncomeGroupId,
                    IncomeGroup = e.IncomeGroup!,
                    UserId = e.UserId,
                    User = new UserResponse
                    {
                        Username = e.User!.Username
                    }
                })
                .ToListAsync();

            return new PagedResponseDto<List<IncomeResponse>>(pagedData, validFilter.PageNumber, validFilter.PageSize);
        }
        catch (Exception ex)
        {
            logger.LogError($"GetIncomesAsync: An error occurred. Error: {ex.Message}");
            throw;
        }
    }

    public async Task<List<Income>> GetLatestIncomesAsync()
    {
        try
        {
            return await context.Incomes
                .Include(e => e.User)
                .OrderByDescending(e => e.CreatedAt)
                .Take(5)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            logger.LogError($"GetLatestIncomesAsync: An error occurred. Error: {ex.Message}");
            throw;
        }
    }

    public async Task<Response<Income>?> GetIncomeAsync(int id)
    {
        try
        {
            var income = await context.Incomes
                .Where(e => e.Id == id)
                .FirstOrDefaultAsync();

            if (income == null) return null;

            return new Response<Income>(income);
        }
        catch (Exception ex)
        {
            logger.LogError($"GetIncomeAsync: An error occurred. Error: {ex.Message}");
            throw;
        }
    }

    public async Task<ActionResult<Income>> CreateIncomeAsync(Income income, ControllerBase controller)
    {
        try
        {
            var validationResult = await validator.ValidateAsync(income);
            if (!validationResult.IsValid) return new BadRequestObjectResult(validationResult.Errors);

            _ = await context.IncomeGroups.FindAsync(income.IncomeGroupId) ??
                throw NotFoundException.Create("IncomeGroupId", "Income group not found.");

            var userId = getAuthenticatedUserId.GetUserId(controller.User);

            income.UserId = (int)userId!;

            context.Incomes.Add(income);

            await context.SaveChangesAsync();

            return new CreatedAtActionResult("GetIncome", "Income", new { id = income.Id }, income);
        }
        catch (Exception ex)
        {
            logger.LogError($"CreateIncomeAsync: An error occurred. Error: {ex.Message}");
            throw;
        }
    }

    public async Task<IActionResult> UpdateIncomeAsync(int id, Income income, ControllerBase controller)
    {
        try
        {
            if (id != income.Id) return new BadRequestResult();

            var authenticatedUserId = getAuthenticatedUserId.GetUserId(controller.User);

            // Check if authenticatedUserId has a value
            if (authenticatedUserId.HasValue)
            {
                // Attach authenticated user id
                income.UserId = authenticatedUserId.Value;

                context.Entry(income).State = EntityState.Modified;

                try
                {
                    await context.SaveChangesAsync();
                }
                catch (ConflictException)
                {
                    if (!IncomeExists(id)) return new NotFoundResult();
                    throw new ConflictException("IncomeService.cs");
                }

                return new NoContentResult();
            }

            return new BadRequestResult();
        }
        catch (Exception ex)
        {
            logger.LogError($"UpdateIncomeAsync: An error occurred. Error: {ex.Message}");
            throw;
        }
    }

    public async Task<IActionResult> DeleteIncomeByIdAsync(int id)
    {
        try
        {
            var income = await context.Incomes.FindAsync(id);

            if (income == null) return new NotFoundResult();

            context.Incomes.Remove(income);
            await context.SaveChangesAsync();

            return new NoContentResult();
        }
        catch (Exception ex)
        {
            logger.LogError($"DeleteIncomeByIdAsync: An error occurred. Error: {ex.Message}");
            throw;
        }
    }

    public async Task<ActionResult<int>> GetTotalAmountOfIncomesAsync()
    {
        try
        {
            return await context.Incomes.CountAsync();
        }
        catch (Exception ex)
        {
            logger.LogError($"GetTotalAmountOfIncomesAsync: An error occurred. Error: {ex.Message}");
            throw;
        }
    }

    private bool IncomeExists(int id)
    {
        try
        {
            return context.Incomes.Any(e => e.Id == id);
        }
        catch (Exception ex)
        {
            logger.LogError($"IncomeExists: An error occurred. Error: {ex.Message}");
            throw;
        }
    }

    private static IQueryable<Income> ApplyFilter(IQueryable<Income> query, Expression<Func<Income, bool>> filter,
        bool condition)
    {
        return condition ? query.Where(filter) : query;
    }
}