using System.Security.Claims;

namespace Domain.Interfaces;

public interface IGetAuthenticatedUserIdService
{
    int? GetUserId(ClaimsPrincipal user);
}