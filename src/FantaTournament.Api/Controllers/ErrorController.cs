using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace FantaTournament.Api.Controllers;

/// <summary>
/// Centralized handler for unhandled exceptions.
/// </summary>
[ApiController]
[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorController : ControllerBase
{
    private readonly ILogger<ErrorController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="ErrorController"/> class.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    public ErrorController(ILogger<ErrorController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Global error handling endpoint.
    /// Configured with app.UseExceptionHandler("/error")
    /// </summary>
    [Route("/error")]
    public IActionResult HandleError()
    {
        var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
        var exception = context?.Error;

        _logger.LogError(exception, "Unhandled exception occurred while processing request to {Path}", HttpContext.Request.Path);

        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "An error occurred while processing your request.",
            Detail = exception?.Message,
            Instance = HttpContext.Request.Path
        };

        // Include stack trace only in development environment
        if (HttpContext.RequestServices.GetRequiredService<IWebHostEnvironment>().IsDevelopment())
        {
            problemDetails.Extensions["stackTrace"] = exception?.StackTrace;
            problemDetails.Extensions["exceptionType"] = exception?.GetType().Name;
        }

        return StatusCode(StatusCodes.Status500InternalServerError, problemDetails);
    }
}
