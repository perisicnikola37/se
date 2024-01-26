using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Service
{
    public interface IIncomeGroupService
    {
        Task<IEnumerable<object>> GetIncomeGroupsAsync();
        Task<ActionResult<IncomeGroup>> GetIncomeGroupAsync(int id);
        Task<ActionResult<IncomeGroup>> CreateIncomeGroupAsync(IncomeGroup incomeGroup, ControllerBase controller);
        Task<IActionResult> UpdateIncomeGroupAsync(int id, IncomeGroup incomeGroup);
        Task<IActionResult> DeleteIncomeGroupByIdAsync(int id);
    }
}
