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
            // Example policy based on 'permissions' claim as per Auth0 best practices
            options.AddPolicy("AdminOnly", policy => 
                policy.RequireClaim("permissions", "admin:all"));
            
            options.AddPolicy("UserAccess", policy => 
                policy.RequireClaim("permissions", "user:access"));

            options.AddPolicy("BoardReader", policy => 
                policy.RequireClaim("permissions", "read:boards"));

            options.AddPolicy("BoardWriter", policy => 
                policy.RequireClaim("permissions", "write:boards"));
        });

        return services;
    }
}
