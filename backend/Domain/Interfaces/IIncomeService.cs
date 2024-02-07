using Contracts.Dto;
using Contracts.Filter;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Domain.Interfaces;

public interface IIncomeService
{
	Task<PagedResponseDto<List<IncomeResponseDto>>> GetIncomesAsync(PaginationFilterDto filter);
	Task<ActionResult<IEnumerable<Income>>> GetLatestIncomesAsync();
	Task<ActionResult<Income>> GetIncomeAsync(int id);
	Task<ActionResult<Income>> CreateIncomeAsync(Income income);
	Task<IActionResult> UpdateIncomeAsync(int id, Income income);
	Task<IActionResult> DeleteIncomeByIdAsync(int id);
	Task<ActionResult<int>> GetTotalAmountOfIncomesAsync();
	Task<IActionResult> DeleteAllIncomesAsync();
}