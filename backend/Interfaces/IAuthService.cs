namespace Vega.Interfaces;
using Vega.Models;

public interface IAuthService
{
    Task<LoggedInUser> LogInUserAsync(LogInUser user);
}
