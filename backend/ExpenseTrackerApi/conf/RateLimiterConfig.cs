using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;

public static class RateLimiterConfig
{
    public static void ConfigureRateLimiter(this IServiceCollection services)
    {
        services.AddRateLimiter(rateLimiterOptions => rateLimiterOptions
            .AddFixedWindowLimiter("fixed", options =>
            {
                options.PermitLimit = 4;
                options.Window = TimeSpan.FromSeconds(12);
                options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                options.QueueLimit = 2;
            }));
    }
}
