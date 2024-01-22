using System.Security.Claims;
using Domain.Interfaces;

namespace Service;

public class GetAuthenticatedUserIdService : IGetAuthenticatedUserIdService
{
    public int? GetUserId(ClaimsPrincipal user)
    {
        var userIdClaim = user.Claims.FirstOrDefault(c => c.Type == "Id");
        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var userId))
            return null;
        return userId;
    }
}