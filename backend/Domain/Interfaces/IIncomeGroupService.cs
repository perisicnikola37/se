using Contracts.Dto;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Domain.Interfaces
{
    public interface IIncomeGroupService
    {
        Task<IEnumerable<IncomeGroupDto>> GetIncomeGroupsAsync();
        Task<ActionResult<IncomeGroup>> GetIncomeGroupAsync(int id);
        Task<ActionResult<IncomeGroup>> CreateIncomeGroupAsync(IncomeGroup incomeGroup);
        Task<IActionResult> UpdateIncomeGroupAsync(int id, IncomeGroup incomeGroup);
        Task<IActionResult> DeleteIncomeGroupByIdAsync(int id);
    }
}
