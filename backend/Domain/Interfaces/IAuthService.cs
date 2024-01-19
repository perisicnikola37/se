namespace Domain.Interfaces;

using Microsoft.AspNetCore.Mvc;
using Domain.Models;

public interface IAuthService
{
    Task<LoggedInUser> LogInUserAsync(LogInUser user);
    Task<ActionResult<User>> RegisterUserAsync(User userRegistration);
}
