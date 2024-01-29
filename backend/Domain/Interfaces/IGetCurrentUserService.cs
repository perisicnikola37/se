using Contracts.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Domain.Interfaces
{
    public interface IGetCurrentUserService
    {
        ActionResult<UserDto> GetCurrentUser();
    }
}
