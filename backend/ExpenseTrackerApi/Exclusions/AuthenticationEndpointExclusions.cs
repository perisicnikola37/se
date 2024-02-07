namespace ExpenseTrackerApi.Exclusions;

public static class AuthenticationEndpointExclusions
{
	public static List<string> ExcludedEndpoints =>
	[
		// "/api/auth/login",
		// "/api/auth/register",
		// "/api/auth/forgot/password",
		// "/api/auth/reset/password",
	];
}