using Contracts.Dto;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Domain.Interfaces;

public interface IAuthService
{
    Task<UserDto?> LogInUserAsync(LogInUser user);
    Task<ActionResult<UserDto>> RegisterUserAsync(User userRegistration);
}