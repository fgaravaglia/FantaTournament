using System.Reflection;
using Microsoft.OpenApi.Models;

namespace FantaTournament.Api.Extensions;

/// <summary>
/// Extension methods for Swagger configuration.
/// </summary>
public static class SwaggerExtensions
{
    /// <summary>
    /// Configures Swagger with automatic documentation from XML comments.
    /// </summary>
    public static IServiceCollection AddApiSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "FantaTournament API",
                Version = "v1",
                Description = "REST API for FantaTournament - Football Forecast Management",
                Contact = new OpenApiContact
                {
                    Name = "Francesco Garavaglia",
                    Url = new Uri("https://github.com/fgaravaglia/FantaTournament")
                }
            });

            // Include XML comments for Swagger documentation
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            
            if (File.Exists(xmlPath))
            {
                options.IncludeXmlComments(xmlPath);
            }

            // Order endpoints by controller
            options.OrderActionsBy(desc => $"{desc.ActionDescriptor.RouteValues["controller"]}_{desc.HttpMethod}");
        });

        return services;
    }
}
