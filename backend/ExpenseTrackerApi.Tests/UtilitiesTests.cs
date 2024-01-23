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

		[Fact]
		public void VerifyPassword_ReturnsTrueForValidPassword()
		{
			var originalPassword = "testPassword";
			var hashedPassword = AuthService.HashPassword(originalPassword);

			var result = AuthService.VerifyPassword(originalPassword, hashedPassword);

			Assert.True(result);
		}

		[Fact]
		public void VerifyPassword_ReturnsFalseForInvalidPassword()
		{
			var originalPassword = "testPassword";
			var hashedPassword = AuthService.HashPassword(originalPassword);
			var wrongPassword = "wrongPassword";

			var result = AuthService.VerifyPassword(wrongPassword, hashedPassword);

			Assert.False(result);
		}
	}
}
