using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace FantaTournament.Api.Extensions;

/// <summary>
/// Extension methods for configuring Authentication and Authorization.
/// </summary>
public static class AuthExtensions
{
    /// <summary>
    /// Configures JWT Bearer authentication using Auth0.
    /// </summary>
    public static IServiceCollection AddApiAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var domain = configuration["Auth0:Domain"];
        var audience = configuration["Auth0:Audience"];

        if (string.IsNullOrEmpty(domain) || string.IsNullOrEmpty(audience))
        {
            return services;
        }

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = $"https://{domain}/";
                options.Audience = audience;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true
                };
            });

        return services;
    }

    /// <summary>
    /// Configures role-based or permission-based authorization policies.
    /// </summary>
    public static IServiceCollection AddApiAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            // Scopes as per requirements:
            // - read:boards: Access and read board data
            // - write:boards: Update match results
            // - read:forecast: View user forecasts
            // - write:forecast: Update forecasts
            // - read:admin: Access admin pages
            // - write:admin: Update configuration/write access to admin pages

            options.AddPolicy("NormalUser", policy => 
                policy.RequireClaim("permissions", "read:boards", "read:forecast", "write:forecast"));
            
            options.AddPolicy("Administrator", policy => 
                policy.RequireClaim("permissions", "write:boards", "read:admin", "write:admin"));
        });

        return services;
    }
}
