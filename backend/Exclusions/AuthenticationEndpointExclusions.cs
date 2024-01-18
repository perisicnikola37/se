public static class AuthenticationEndpointExclusions
{
	public static List<string> ExcludedEndpoints => new List<string>
	{
		"/api/Auth/Login",
		"/api/Auth/Register"
	};
}

