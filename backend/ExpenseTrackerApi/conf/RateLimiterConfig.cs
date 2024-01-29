using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;

namespace ExpenseTrackerApi.conf;

public static class RateLimiterConfig
{
    public static void ConfigureRateLimiter(this IServiceCollection services)
    {
        services.AddRateLimiter(rateLimiterOptions => rateLimiterOptions
            .AddFixedWindowLimiter("fixed", options =>
            {
                options.PermitLimit = 5;
                options.Window = TimeSpan.FromSeconds(10);
                options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                options.QueueLimit = 2;
            }));
    }
}