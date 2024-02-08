namespace ExpenseTrackerApi.conf;

using ExpenseTrackerApi.Handlers;
using Microsoft.AspNetCore.Authorization;

public static class AuthorizationHandlerConfig
{
	public static void ConfigureAuthorizationHandlers(this IServiceCollection services)
	{
		services.AddScoped<IAuthorizationHandler, BlogAuthorizationHandler>();
		services.AddScoped<IAuthorizationHandler, ExpenseAuthorizationHandler>();
		services.AddScoped<IAuthorizationHandler, IncomeAuthorizationHandler>();
		services.AddScoped<IAuthorizationHandler, IncomeGroupAuthorizationHandler>();
		services.AddScoped<IAuthorizationHandler, ExpenseGroupAuthorizationHandler>();
	}
}
