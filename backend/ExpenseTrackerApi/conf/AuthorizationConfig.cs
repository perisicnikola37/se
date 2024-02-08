namespace ExpenseTrackerApi.conf;

using ExpenseTrackerApi.Handlers;
using Microsoft.AspNetCore.Authorization;

public static class AuthorizationConfig
{
	public static void ConfigureAuthorizationPolicies(this AuthorizationOptions options)
	{
		options.AddPolicy("BlogOwnerPolicy", policy =>
		{
			policy.Requirements.Add(new BlogAuthorizationRequirement());
		});

		options.AddPolicy("ExpenseOwnerPolicy", policy =>
		{
			policy.Requirements.Add(new ExpenseAuthorizationRequirement());
		});

		options.AddPolicy("IncomeOwnerPolicy", policy =>
		{
			policy.Requirements.Add(new IncomeAuthorizationRequirement());
		});

		options.AddPolicy("IncomeGroupOwnerPolicy", policy =>
		{
			policy.Requirements.Add(new IncomeGroupAuthorizationRequirement());
		});

		options.AddPolicy("ExpenseGroupOwnerPolicy", policy =>
		{
			policy.Requirements.Add(new ExpenseGroupAuthorizationRequirement());
		});
	}
}
