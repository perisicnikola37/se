using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Domain.Interfaces
{
    public interface IGetCurrentUserService
    {
        ActionResult<LoggedInUser> GetCurrentUser();
    }
}
