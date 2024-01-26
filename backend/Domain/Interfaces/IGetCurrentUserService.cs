using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Service
{
    public interface IGetCurrentUserService
    {
        ActionResult<LoggedInUser> GetCurrentUser();
    }
}
