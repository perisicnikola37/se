using Microsoft.EntityFrameworkCore;
using Vega.Classes;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Vega.Interfaces;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Services.BuildServiceProvider().GetService<IConfiguration>();
// disabled
// builder.Services.AddHttpLogging(o => { });
builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(
		policy =>
		{
			policy.WithOrigins("http://example.com");
		});
});

// add DB context
string connectionString = configuration["DefaultConnection"];

builder.Services.AddDbContext<MainDatabaseContext>(options =>
{
	options.UseMySql(
	  connectionString,
		new MySqlServerVersion(new Version(8, 0, 35))
	);
});

builder.Services.AddAuthentication();

// important for adding routes based on controllers
builder.Services.AddControllers().AddNewtonsoftJson(options =>
	options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

// services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<EmailService>();
builder.Services.AddScoped<GetAuthenticatedUserIdService>();


builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
	string validIssuer = configuration["Jwt:Issuer"];
	string validAudience = configuration["Jwt:Audience"];
	string issuerSigningKey = configuration["Jwt:Key"];

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

// app.UseHttpLogging();
app.UseRateLimiter();

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

// this middleware needs to be after .net auth middlewares!
app.UseMiddleware<ClaimsMiddleware>();

// cors
app.UseCors();

app.Run();

