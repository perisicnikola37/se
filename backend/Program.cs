using Microsoft.EntityFrameworkCore;
using Vega.classes;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpLogging(o => { });

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("http://example.com",
                                "http://www.contoso.com");
        });
});

// add DB context
builder.Services.AddDbContext<MyDBContext>(options =>
{
    options.UseMySql(
       "server=localhost;user=root;port=3306;Connect Timeout=5;database=first_database;password=06032004",
        new MySqlServerVersion(new Version(8, 0, 35))
    );
});

builder.Services.AddAuthentication();

// important for adding routes based on controllers
builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

builder.Services.AddAuthentication(options =>
    {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],

        IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRateLimiter(_ => _
    .AddFixedWindowLimiter(policyName: "fixed", options =>
    {
        options.PermitLimit = 4;
        options.Window = TimeSpan.FromSeconds(12);
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        options.QueueLimit = 2;
    }));

var app = builder.Build();

app.UseHttpLogging();
app.UseRateLimiter();

static string GetTicks() => (DateTime.Now.Ticks & 0x11111).ToString("00000");

app.MapGet("/", () => Results.Ok($"Hello {GetTicks()}"))
                           .RequireRateLimiting("fixed");

app.MapGet("/security/getMessage",
() => "Hello World!").RequireAuthorization();

// app.MapPost("/security/createToken",
// [AllowAnonymous] (User user) =>
// {
//     if (user.username == "nikolaperisic@gmail.com" && user.password == "06032004")
//     {
//         var issuer = builder.Configuration["Jwt:Issuer"];
//         var audience = builder.Configuration["Jwt:Audience"];
//         var key = Encoding.ASCII.GetBytes
//         (builder.Configuration["Jwt:Key"]);
//         var tokenDescriptor = new SecurityTokenDescriptor
//         {
//             Subject = new ClaimsIdentity(new[]
//             {
//                 new Claim("Id", Guid.NewGuid().ToString()),
//                 new Claim(JwtRegisteredClaimNames.Sub, user.username),
//                 new Claim(JwtRegisteredClaimNames.Email, user.username),
//                 new Claim(JwtRegisteredClaimNames.Jti,
//                 Guid.NewGuid().ToString())
//             }),
//             Expires = DateTime.UtcNow.AddMinutes(5),
//             Issuer = issuer,
//             Audience = audience,
//             SigningCredentials = new SigningCredentials
//             (new SymmetricSecurityKey(key),
//             SecurityAlgorithms.HmacSha512Signature)
//         };
//         var tokenHandler = new JwtSecurityTokenHandler();
//         var token = tokenHandler.CreateToken(tokenDescriptor);
//         var jwtToken = tokenHandler.WriteToken(token);
//         var stringToken = tokenHandler.WriteToken(token);
//         return Results.Ok(stringToken);
//     }
//     return Results.Unauthorized();
// });


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

// auth
app.UseAuthentication();
app.UseAuthorization();

// cors
app.UseCors();

app.Run();

