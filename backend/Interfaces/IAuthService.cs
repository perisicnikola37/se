namespace Vega.Interfaces;

using Microsoft.AspNetCore.Mvc;
using Vega.Models;

public interface IAuthService
{
    Task<LoggedInUser> LogInUserAsync(LogInUser user);
    Task<ActionResult<User>> RegisterUserAsync(User userRegistration);
}
