using Contracts.Dto;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Domain.Interfaces
{
    public interface IIncomeGroupService
    {
        Task<IEnumerable<IncomeGroupDto>> GetIncomeGroupsAsync(ControllerBase controller);
        Task<ActionResult<IncomeGroup>> GetIncomeGroupAsync(int id);
        Task<ActionResult<IncomeGroup>> CreateIncomeGroupAsync(IncomeGroup incomeGroup, ControllerBase controller);
        Task<IActionResult> UpdateIncomeGroupAsync(int id, IncomeGroup incomeGroup, ControllerBase controller);
        Task<IActionResult> DeleteIncomeGroupByIdAsync(int id);
    }
}
