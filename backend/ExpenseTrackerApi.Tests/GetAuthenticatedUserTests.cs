using System.Security.Claims;

namespace ExpenseTrackerApi.Tests;
	public class GetAuthenticatedUserIdServiceTests
	{
		[Fact]
		public void GetUserId_ShouldReturnUserId_WhenValidClaimsProvided()
		{
			var service = new GetAuthenticatedUserIdService();
			var claims = new List<Claim>
			{
				new("Id", "123")
			};
			var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "TestAuthentication"));

			var userId = service.GetUserId(user);

			Assert.NotNull(userId);
			Assert.Equal(123, userId);
		}

		[Fact]
		public void GetUserId_ShouldReturnNull_WhenNoIdClaim()
		{
			var service = new GetAuthenticatedUserIdService();
			var user = new ClaimsPrincipal(new ClaimsIdentity());

			var userId = service.GetUserId(user);

			Assert.Null(userId);
		}
		
		[Fact]
		public void GetUserId_ShouldReturnNull_WhenInvalidIdClaim()
		{
			var service = new GetAuthenticatedUserIdService();
			var claims = new List<Claim>
			{
				new("Id", "invalid")
			};
			var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "TestAuthentication"));

			var userId = service.GetUserId(user);

			Assert.Null(userId);
		}
	}
