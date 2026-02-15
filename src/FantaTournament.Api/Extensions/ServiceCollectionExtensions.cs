using System.Text.Json;
using System.Text.Json.Serialization;
using FantaTournament.Application;
using FantaTournament.Infrastructure;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Umbrella.Core;

namespace FantaTournament.Api.Extensions;

/// <summary>
/// Extension methods for <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers all API, Application, and Infrastructure services.
    /// </summary>
    public static IServiceCollection AddApiServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Register Umbrella Core (Message Bus, etc.)
        services.AddUmbrellaCore();

        // Add FantaTournament business dependencies
        services.AddFantaTournament(configuration);

        // Configure Authentication & Authorization
        services.AddApiAuthentication(configuration);
        services.AddApiAuthorization();

        // Configure Controllers with REST best practices
        services.AddControllers()
            .AddJsonOptions(options =>
            {
                // camelCase for properties (REST standard)
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                
                // Ignore nulls in responses
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                
                // Enums as strings
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                
                // Indented for dev, but standard is compact
                options.JsonSerializerOptions.WriteIndented = false;
            });

        services.AddEndpointsApiExplorer();

        // Configure CORS
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });
        });

        return services;
    }

    /// <summary>
    /// Configures health checks for infrastructure monitoring.
    /// </summary>
    public static IServiceCollection AddApiHealthChecks(this IServiceCollection services)
    {
        services.AddHealthChecks()
            .AddCheck("self", () => HealthCheckResult.Healthy());
        
        return services;
    }
}
