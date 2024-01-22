using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Domain.Interfaces;

public interface IAuthService
{
    Task<LoggedInUser?> LogInUserAsync(LogInUser user);
    Task<ActionResult<User>> RegisterUserAsync(User userRegistration);
}