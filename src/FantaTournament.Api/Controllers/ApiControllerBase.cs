using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Umbrella.Core;

namespace FantaTournament.Api.Controllers;

/// <summary>
/// Base class for API controllers providing helper methods for mapping application results.
/// </summary>
[ApiController]
[Authorize]
[Route("api/[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
    /// <summary>
    /// Maps a <see cref="Result{T}"/> to an <see cref="ActionResult"/>.
    /// </summary>
    /// <typeparam name="T">The type of data in the result.</typeparam>
    /// <param name="result">The result to map.</param>
    /// <returns>
    /// An <see cref="OkObjectResult"/> if successful, 
    /// a <see cref="NotFoundResult"/> if not found, 
    /// or a <see cref="BadRequestObjectResult"/> with errors if failed.
    /// </returns>
    protected ActionResult MapToActionResult<T>(Result<T> result)
    {
        if (result.Succeeded)
        {
            return Ok(result.Data);
        }

        if (result.Errors.Contains("Not Found", StringComparer.OrdinalIgnoreCase))
        {
            return NotFound();
        }

        return BadRequest(new { errors = result.Errors });
    }
}
