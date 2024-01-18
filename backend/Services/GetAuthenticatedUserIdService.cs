using System.Security.Claims;

public interface IGetAuthenticatedUserIdService
{
	int? GetUserId(ClaimsPrincipal user);
}

// return Unauthorized(new { message = "Invalid user claims" });

public class GetAuthenticatedUserIdService : IGetAuthenticatedUserIdService
{
	public int? GetUserId(ClaimsPrincipal user)
	{
		var userIdClaim = user.Claims.FirstOrDefault(c => c.Type == "Id");
		if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
		{
			return null;
		} else 
		{
			return userId;
		}
	}
}
