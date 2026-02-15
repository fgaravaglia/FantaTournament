using FantaTournament.Application.Commands;
using FantaTournament.Application.DTOs;
using FantaTournament.Application.Queries;
using Microsoft.AspNetCore.Mvc;

namespace FantaTournament.Api.Controllers;

/// <summary>
/// Controller for managing user Forecasts.
/// </summary>
public class ForecastsController : ApiControllerBase
{
    private readonly IForecastQueries _forecastQueries;
    private readonly IForecastCommands _forecastCommands;

    public ForecastsController(IForecastQueries forecastQueries, IForecastCommands forecastCommands)
    {
        _forecastQueries = forecastQueries;
        _forecastCommands = forecastCommands;
    }

    /// <summary>
    /// Retrieves a forecast by its unique identifier.
    /// </summary>
    /// <param name="id">The ID of the forecast.</param>
    /// <returns>The forecast data.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<ForecastDto>> GetForecastById(string id)
    {
        var result = await _forecastQueries.GetForecastByIdAsync(id);
        return MapToActionResult(result);
    }

    /// <summary>
    /// Retrieves all forecasts for a specific user.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <returns>A collection of forecasts for the user.</returns>
    [HttpGet("user/{userId}")]
    public async Task<ActionResult<IEnumerable<ForecastDto>>> GetForecastsByUserId(string userId)
    {
        var result = await _forecastQueries.GetForecastsByUserIdAsync(userId);
        return MapToActionResult(result);
    }

    /// <summary>
    /// Creates a new forecast.
    /// </summary>
    /// <param name="forecastDto">The forecast data to create.</param>
    /// <returns>The ID of the created forecast.</returns>
    [HttpPost]
    public async Task<ActionResult<string>> CreateForecast([FromBody] ForecastDto forecastDto)
    {
        var result = await _forecastCommands.CreateForecastAsync(forecastDto);
        return MapToActionResult(result);
    }

    /// <summary>
    /// Updates an existing forecast.
    /// </summary>
    /// <param name="id">The ID of the forecast to update.</param>
    /// <param name="forecastDto">The updated forecast data.</param>
    /// <returns>The ID of the updated forecast.</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<string>> UpdateForecast(string id, [FromBody] ForecastDto forecastDto)
    {
        // Ensure the ID in the URL matches the ID in the body if applicable, 
        // though typically the Command handler should handle this or we can enforce it here.
        var result = await _forecastCommands.UpdateForecastAsync(forecastDto);
        return MapToActionResult(result);
    }

    /// <summary>
    /// Deletes a specific forecast.
    /// </summary>
    /// <param name="id">The ID of the forecast to delete.</param>
    /// <returns>Confirmation of deletion.</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<string>> DeleteForecast(string id)
    {
        var result = await _forecastCommands.DeleteForecastAsync(id);
        return MapToActionResult(result);
    }
}
