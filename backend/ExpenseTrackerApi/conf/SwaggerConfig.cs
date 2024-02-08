using Microsoft.OpenApi.Models;

namespace ExpenseTrackerApi.conf;

public static class SwaggerConfig
{
    public static void ConfigureSwaggerGen(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Expense Tracker",
                Version = "v1.0",
                Description = ".NET and React-based application designed to help users manage and track their expenses and incomes effectively. The goal is to provide a comprehensive solution for organizing financial transactions, generating reports, and offering additional premium features for an enhanced user experience.",
                Contact = new OpenApiContact
                {
                    Name = "Nikola Perisic",
                    Email = "nikola@e-invoices.online",
                    Url = new Uri("https://github.com/perisicnikola37")
                },
                License = new OpenApiLicense
                {
                    Name = "LinkedIn",
                    Url = new Uri("https://www.linkedin.com/in/perisicnikola37")
                }
            });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme."
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
    }
}