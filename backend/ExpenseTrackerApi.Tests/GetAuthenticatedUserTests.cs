using System.Security.Claims;

namespace ExpenseTrackerApi.Tests;
public class GetAuthenticatedUserIdServiceTests
{
	[Fact]
	public void GetUserId_ShouldReturnUserId_WhenValidClaimsProvided()
	{
		// Arrange
		// Create an instance of the GetAuthenticatedUserIdService 
		var service = new GetAuthenticatedUserIdService();
		// Create claims with a user ID for testing
		var claims = new List<Claim>
		{
			new("Id", "123")
		};

		// Create a ClaimsPrincipal with the test claims
		var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "TestAuthentication"));

		// Act
		var userId = service.GetUserId(user);

		// Assert
		Assert.NotNull(userId);
		Assert.Equal(123, userId);
	}

	[Fact]
	public void GetUserId_ShouldReturnNull_WhenNoIdClaim()
	{
		// Arrange
		// Create an instance of the GetAuthenticatedUserIdService under test
		var service = new GetAuthenticatedUserIdService();

		// Create a ClaimsPrincipal without any claims
		var user = new ClaimsPrincipal(new ClaimsIdentity());

		// Act
		var userId = service.GetUserId(user);

		// Assert
		Assert.Null(userId);
	}

	[Fact]
	public void GetUserId_ShouldReturnNull_WhenInvalidIdClaim()
	{
		// Arrange
		var service = new GetAuthenticatedUserIdService();

		// Create claims with an invalid (non-numeric) user ID for testing
		var claims = new List<Claim>
		{
		new("Id", "invalid")
		};

		// Create a ClaimsPrincipal with the test claims
		var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "TestAuthentication"));

		// Act
		var userId = service.GetUserId(user);

		// Assert
		Assert.Null(userId);
	}
}
