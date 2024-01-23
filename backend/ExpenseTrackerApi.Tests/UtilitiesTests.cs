using Service;

namespace ExpenseTrackerApi.Tests
{
	public class UtilitiesTests
	{
		[Fact]
		public void HashPassword_ReturnsHashedString()
		{
			var password = "testPassword";

			var actualHash = AuthService.HashPassword(password);

			Assert.NotEqual(password, actualHash, StringComparer.OrdinalIgnoreCase);
		}
	}
}
