namespace ExpenseTrackerApi.conf;
using Microsoft.AspNetCore.Builder;

public static class RedirectionExtensions
{
    public static void ApplyRedirection(this IApplicationBuilder app)
    {
        app.Use(async (context, next) =>
        {
            if (context.Request.Path == "/")
            {
                context.Response.Redirect("/swagger/index.html");
                return;
            }

            await next();
        });
    }
}
