using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;

public static class JwtConfig
{
    public static void ConfigureJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            var validIssuer = configuration["Jwt:Issuer"] ?? "https://joydipkanjilal.com/";
            var validAudience = configuration["Jwt:Audience"] ?? "https://joydipkanjilal.com/";
            var issuerSigningKey = configuration["Jwt:Key"] ??
                                   "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.	eyJkZGFzYWRoYXNiZCBhc2RhZHMgc2Rhc3AgZGFzIGRhc2RhcyBhc2RhcyBkYXNkIGFzZGFzZGFzZCBhcyBkYXNhZGFzIGFzIGRhcyBkYXNhZGFzIGFzIGRhcyBkYXNhZGFzZGFzZCBhcyBkYXNhIGRhcyBkYXNhIGRhcyBkYXNhIGRhcyBkYXNhIGRhcyBkYXNhIGRhcyBkYXNhIGRhcyBkYXNhIGFzIGRhcyBkYXNhIGRhcyBkYXNhZGFzIGRhcyBkYXNhZGphcyBkYXNhIGRhcyBkYXNhIGRhcyBkYXNhIGRhcyBkYXNhIGRhcyBkYXNhIGRhcyBkYXNhIGRhcyBkYXNhIGRhcyBkYXNhIGRhcyBkYXNhIGRhcyBkYXNhZGphcyIsImlhdCI6MTYzNDEwNTUyMn0.S7G4f8pW7sGJ7t9PIShNElA0RRve-HlPfZRvX8hnZ6c";

            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = validIssuer,
                ValidAudience = validAudience,

                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(issuerSigningKey)),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true
            };
        });
    }
}
