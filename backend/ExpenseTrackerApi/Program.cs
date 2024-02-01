using DinkToPdf;
using DinkToPdf.Contracts;
using Domain.Interfaces;
using Domain.Models;
using Domain.Validators;
using ExpenseTrackerApi;
using ExpenseTrackerApi.conf;
using ExpenseTrackerApi.Handlers;
using ExpenseTrackerApi.Middlewares;
using FluentValidation;
using Infrastructure.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Persistence;
using Presentation;
using Service;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
// disabled
// builder.Services.AddHttpLogging(o => { });
builder.Services.AddCors(p => p.AddPolicy("cors", builder =>
{
	builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
builder.Services.AddScoped<PdfGenerator>();

builder.Services.AddAuthorization();

builder.Services.AddHttpContextAccessor();

// Policies
builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("BlogOwnerPolicy", policy => { policy.Requirements.Add(new BlogAuthorizationRequirement()); });
	options.AddPolicy("ExpenseOwnerPolicy",
		policy => { policy.Requirements.Add(new ExpenseAuthorizationRequirement()); });
	options.AddPolicy("IncomeOwnerPolicy",
		policy => { policy.Requirements.Add(new IncomeAuthorizationRequirement()); });
});

builder.Services.AddScoped<IAuthorizationHandler, BlogAuthorizationHandler>();
builder.Services.AddScoped<IAuthorizationHandler, ExpenseAuthorizationHandler>();
builder.Services.AddScoped<IAuthorizationHandler, IncomeAuthorizationHandler>();

var connectionString = configuration["DefaultConnection"];
if (connectionString == null) throw new ArgumentNullException(nameof(connectionString), "DefaultConnection is null");

builder.Services.AddDbContext<DatabaseContext>(options =>
{
	options.UseMySql(
		connectionString,
		new MySqlServerVersion(new Version(8, 0, 35)),
		b => b.MigrationsAssembly("ExpenseTrackerApi")
	);
});

builder.Services.AddAuthentication();

// important for adding routes based on controllers
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
	options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
	options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
});

// validators
builder.Services.AddScoped<IValidator<Blog>, BlogValidator>();
builder.Services.AddScoped<IValidator<ExpenseGroup>, ExpenseGroupValidator>();
builder.Services.AddScoped<IValidator<Expense>, ExpenseValidator>();
builder.Services.AddScoped<IValidator<IncomeGroup>, IncomeGroupValidator>();
builder.Services.AddScoped<IValidator<Income>, IncomeValidator>();
builder.Services.AddScoped<IValidator<User>, UserValidator>();
builder.Services.AddScoped<IValidator<Reminder>, ReminderValidator>();

// services

// core
builder.Services.AddScoped<IDatabaseContext, DatabaseContext>();
builder.Services.AddScoped<IGetAuthenticatedUserIdService, GetAuthenticatedUserIdService>();
builder.Services.AddScoped<IGetCurrentUserService, GetCurrentUserService>();
builder.Services.AddScoped<GetAuthenticatedUserIdService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IEmailService, EmailService>();

// main entities
builder.Services.AddScoped<IExpenseGroupService, ExpenseGroupService>();
builder.Services.AddScoped<IExpenseService, ExpenseService>();

builder.Services.AddScoped<IIncomeGroupService, IncomeGroupService>();
builder.Services.AddScoped<IIncomeService, IncomeService>();

// other entitites
builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddScoped<IReminderService, ReminderService>();

// repositories
builder.Services.AddScoped<ReminderRepository>();

// Jwt configuration
builder.Services.ConfigureJwtAuthentication(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();

// Swagger configuration
builder.Services.ConfigureSwaggerGen();

// Rate Limiter configuration
// builder.Services.ConfigureRateLimiter();

var app = builder.Build();

// app.UseHttpLogging();

// handle default route path - redirection to swagger documentation
app.Use(async (context, next) =>
{
	if (context.Request.Path == "/")
	{
		context.Response.Redirect("/swagger/index.html");

		return;
	}

	await next();
});

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Cors had to be before auth
app.UseCors("cors");

// auth
app.UseAuthentication();
app.UseAuthorization();

// app.UseRateLimiter();

app.MapControllers();

// this middleware needs to be after .net auth middlewares!
app.UseMiddleware<ClaimsMiddleware>();
app.UseGlobalExceptionHandler();
app.UseMiddleware<TimeTrackingMiddleware>();


app.Run();
