namespace ExpenseTrackerApi.Tests;
public class UtilitiesTests
{
	[Fact]
	public void HashPassword_ReturnsHashedString()
	{
		// Arrange
		var password = "testPassword";

		// Act
		var actualHash = AuthService.HashPassword(password);

		// Assert
		Assert.NotEqual(password, actualHash, StringComparer.OrdinalIgnoreCase);
	}

	[Fact]
	public void VerifyPassword_ReturnsTrueForValidPassword()
	{
		// Arrange
		var originalPassword = "testPassword";
		var hashedPassword = AuthService.HashPassword(originalPassword);

		// Act
		var result = AuthService.VerifyPassword(originalPassword, hashedPassword);

		// Assert
		Assert.True(result);
	}

	[Fact]
	public void VerifyPassword_ReturnsFalseForInvalidPassword()
	{
		// Arrange
		var originalPassword = "testPassword";
		var hashedPassword = AuthService.HashPassword(originalPassword);
		var wrongPassword = "wrongPassword";

		// Act
		var result = AuthService.VerifyPassword(wrongPassword, hashedPassword);

		// Assert
		Assert.False(result);
	}
}

