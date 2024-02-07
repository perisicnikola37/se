using Contracts.Dto;
using Contracts.Filter;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Domain.Interfaces;

public interface IIncomeService
{
	Task<PagedResponseDto<List<IncomeResponseDto>>> GetIncomesAsync(PaginationFilterDto filter, ControllerBase
	controller);
	Task<object> GetLatestIncomesAsync(ControllerBase controller);
	Task<ActionResult<Income>> GetIncomeAsync(int id);
	Task<ActionResult<Income>> CreateIncomeAsync(Income income, ControllerBase controller);
	Task<IActionResult> UpdateIncomeAsync(int id, Income income, ControllerBase controller);
	Task<IActionResult> DeleteIncomeByIdAsync(int id);
	Task<ActionResult<int>> GetTotalAmountOfIncomesAsync();
	Task<IActionResult> DeleteAllIncomesAsync(ControllerBase controller);
}