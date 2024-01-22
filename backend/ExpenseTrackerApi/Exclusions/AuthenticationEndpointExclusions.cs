namespace ExpenseTrackerApi.Exclusions;

public static class AuthenticationEndpointExclusions
{
    public static List<string> ExcludedEndpoints =>
    [
        "/api/Auth/Login",
        "/api/Auth/Register"
    ];
}