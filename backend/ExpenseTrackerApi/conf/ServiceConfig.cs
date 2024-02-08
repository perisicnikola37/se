namespace ExpenseTrackerApi.conf;

using Domain.Interfaces;
using Domain.Models;
using Domain.Validators;
using Infrastructure.Contexts;
using Service;
using FluentValidation;
using Persistence;

public static class ServiceConfig
{
	public static void ConfigureServices(this IServiceCollection services)
	{
		// Validators
		services.AddScoped<IValidator<Blog>, BlogValidator>();
		services.AddScoped<IValidator<ExpenseGroup>, ExpenseGroupValidator>();
		services.AddScoped<IValidator<Expense>, ExpenseValidator>();
		services.AddScoped<IValidator<IncomeGroup>, IncomeGroupValidator>();
		services.AddScoped<IValidator<Income>, IncomeValidator>();
		services.AddScoped<IValidator<User>, UserValidator>();
		services.AddScoped<IValidator<Reminder>, ReminderValidator>();

		// Core services
		services.AddScoped<IDatabaseContext, DatabaseContext>();
		services.AddScoped<IGetAuthenticatedUserIdService, GetAuthenticatedUserIdService>();
		services.AddScoped<IGetCurrentUserService, GetCurrentUserService>();
		services.AddScoped<GetAuthenticatedUserIdService>();
		services.AddScoped<IAuthService, AuthService>();
		services.AddScoped<IEmailService, EmailService>();

		// Main entities
		services.AddScoped<IExpenseGroupService, ExpenseGroupService>();
		services.AddScoped<IExpenseService, ExpenseService>();

		services.AddScoped<IIncomeGroupService, IncomeGroupService>();
		services.AddScoped<IIncomeService, IncomeService>();

		// Other entities
		services.AddScoped<IBlogService, BlogService>();
		services.AddScoped<IReminderService, ReminderService>();
		services.AddScoped<ISummaryService, SummaryService>();

		// Repositories
		services.AddScoped<ReminderRepository>();
	}
}
