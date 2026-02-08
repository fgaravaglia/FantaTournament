using Serilog;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;

namespace FantaTournament.Api.Extensions;

/// <summary>
/// Extension methods for <see cref="WebApplication"/>.
/// </summary>
public static class WebApplicationExtensions
{
    /// <summary>
    /// Configures the API middleware pipeline.
    /// </summary>
    public static WebApplication ConfigureApiMiddleware(this WebApplication app)
    {
        // Centralized Exception Handling
        app.UseExceptionHandler("/error");

        // Swagger UI
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "FantaTournament API v1");
                options.RoutePrefix = string.Empty; // Serve Swagger at the root
                options.DisplayRequestDuration();
            });
        }

        // HTTPS Redirection
        if (!app.Environment.IsDevelopment())
        {
            app.UseHttpsRedirection();
        }

        // CORS
        app.UseCors("AllowAll");

        // Auth placeholder
        app.UseAuthorization();

        // Structured Logging for Requests
        app.UseSerilogRequestLogging(options =>
        {
            options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
            {
                diagnosticContext.Set("ClientIP", httpContext.Connection.RemoteIpAddress);
                diagnosticContext.Set("UserAgent", httpContext.Request.Headers.UserAgent);
            };
        });

        // Health Checks
        app.MapHealthChecks("/health/ready", new HealthCheckOptions
        {
            Predicate = _ => true,
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });

        app.MapHealthChecks("/health/live", new HealthCheckOptions
        {
            Predicate = check => check.Name == "self"
        });

        // Map Controllers
        app.MapControllers();

        return app;
    }
}
