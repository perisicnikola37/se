using Contracts.Dto;
using Contracts.Filter;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Domain.Interfaces;

public interface IIncomeService
{
	Task<PagedResponseDto<List<IncomeResponse>>> GetIncomesAsync(PaginationFilterDto filter, ControllerBase controller);
	Task<List<Income>> GetLatestIncomesAsync();
	Task<ActionResult<Income>> GetIncomeAsync(int id);
	Task<ActionResult<Income>> CreateIncomeAsync(Income income, ControllerBase controller);
	Task<IActionResult> UpdateIncomeAsync(int id, Income income, ControllerBase controller);
	Task<IActionResult> DeleteIncomeByIdAsync(int id);
	Task<ActionResult<int>> GetTotalAmountOfIncomesAsync();
}