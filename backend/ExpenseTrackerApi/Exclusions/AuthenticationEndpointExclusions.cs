namespace ExpenseTrackerApi.Exclusions;

public static class AuthenticationEndpointExclusions
{
	public static List<string> ExcludedEndpoints =>
	[
		"swagger/index.html",
		"/api/auth/login",
		"/api/auth/register",
		"/api/auth/forgot/password",
		"/api/auth/reset/password",
	];
}