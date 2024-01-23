using System.Security.Claims;

namespace ExpenseTrackerApi.Tests
{
    public class GetAuthenticatedUserIdServiceTests
    {
        private readonly GetAuthenticatedUserIdService _service;

        public GetAuthenticatedUserIdServiceTests()
        {
            _service = new GetAuthenticatedUserIdService();
        }

        [Fact]
        public void GetUserId_ShouldReturnUserId_WhenValidClaimsProvided()
        {
            // Arrange
            var claims = new List<Claim>
            {
                new("Id", "123")
            };

            var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "TestAuthentication"));

            // Act
            var userId = _service.GetUserId(user);

            // Assert
            Assert.NotNull(userId);
            Assert.Equal(123, userId);
        }

        [Fact]
        public void GetUserId_ShouldReturnNull_WhenNoIdClaim()
        {
            // Arrange
            var user = new ClaimsPrincipal(new ClaimsIdentity());

            // Act
            var userId = _service.GetUserId(user);

            // Assert
            Assert.Null(userId);
        }

        [Fact]
        public void GetUserId_ShouldReturnNull_WhenInvalidIdClaim()
        {
            // Arrange
            var claims = new List<Claim>
            {
                new("Id", "invalid")
            };

            var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "TestAuthentication"));

            // Act
            var userId = _service.GetUserId(user);

            // Assert
            Assert.Null(userId);
        }
    }
}
