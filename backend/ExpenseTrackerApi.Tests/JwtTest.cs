using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Moq;

namespace ExpenseTrackerApi.Tests;

public class JwtTokenGeneratorTests
{
    private readonly IConfiguration _configuration;

    public JwtTokenGeneratorTests()
    {
        _configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
                { "Jwt:Issuer", "https://perisicnikola.com/" },
                { "Jwt:Audience", "https://perisicnikola.com/" },
                { "Jwt:Key", GetJwtKey() }
            }!)
            .Build();
    }

    private static string GetJwtKey()
    {
        return
            "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.	eyJkZGFzYWRoYXNiZCBhc2RhZHMgc2Rhc3AgZGFzIGRhc2RhcyBhc2RhcyBkYXNkIGFzZGFzZGFzZCBhcyBkYXNhZGFzIGFzIGRhcyBkYXNhZGFzIGFzIGRhcyBkYXNhZGFzZGFzZCBhcyBkYXNhIGRhcyBkYXNhIGRhcyBkYXNhIGRhcyBkYXNhIGRhcyBkYXNhIGRhcyBkYXNhIGRhcyBkYXNhIGFzIGRhcyBkYXNhIGRhcyBkYXNhZGFzIGRhcyBkYXNhZGphcyBkYXNhIGRhcyBkYXNhIGRhcyBkYXNhIGRhcyBkYXNhIGRhcyBkYXNhIGRhcyBkYXNhIGRhcyBkYXNhIGRhcyBkYXNhIGRhcyBkYXNhIGRhcyBkYXNhZGphcyIsImlhdCI6MTYzNDEwNTUyMn0.S7G4f8pW7sGJ7t9PIShNElA0RRve-HlPfZRvX8hnZ6c";
    }

    [Fact]
    public void GenerateJwtToken_ValidUser_ReturnsValidToken()
    {
        // Arrange
        var dbContextMock = new Mock<DatabaseContext>();
        var emailServiceMock = new Mock<EmailService>(_configuration);

        // Create a mock user object
        var user = new User
        {
            Id = 1,
            Username = "mockUser",
            Email = "mock@example.com"
        };

        // Create an instance of the AuthService with the mock database context and configuration
        var authService = new AuthService(dbContextMock.Object, _configuration, emailServiceMock.Object);

        // Use Reflection to get the private method info
        var methodInfo =
            typeof(AuthService).GetMethod("GenerateJwtToken", BindingFlags.NonPublic | BindingFlags.Instance);
        Assert.NotNull(methodInfo);

        // Act
        var token = (string)methodInfo.Invoke(authService, new object[] { user })!;

        // Assert
        Assert.NotNull(token);
        Assert.NotEmpty(token);

        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(GetJwtKey())),
            ValidIssuer = _configuration["Jwt:Issuer"],
            ValidAudience = _configuration["Jwt:Audience"]
        };

        tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);

        Assert.NotNull(validatedToken);

        // Additional granular assertions about claims
        var jwtToken = validatedToken as JwtSecurityToken;
        Assert.NotNull(jwtToken);

        // Example: Check user ID claim
        var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "Id");
        Assert.NotNull(userIdClaim);
        Assert.Equal(user.Id.ToString(), userIdClaim.Value);

        // Example: Check username claim
        var usernameClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub);
        Assert.NotNull(usernameClaim);
        Assert.Equal(user.Username, usernameClaim.Value);

        // Example: Check email claim
        var emailClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Email);
        Assert.NotNull(emailClaim);
        Assert.Equal(user.Email, emailClaim.Value);
    }
}