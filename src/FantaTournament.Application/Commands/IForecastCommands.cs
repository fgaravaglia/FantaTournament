using FantaTournament.Application.DTOs;
using Umbrella.Core;

namespace FantaTournament.Application.Commands;

/// <summary>
/// Defines commands for managing user forecasts, including creation, updating, and deletion.
/// </summary>
public interface IForecastCommands : ICommandHandler
{
    /// <summary>
    /// Creates a new forecast based on the provided data.
    /// </summary>
    /// <param name="forecastDto">The forecast data to create.</param>
    /// <returns>
    /// A <see cref="Result{T}"/> containing the ID of the created forecast if successful.
    /// </returns>
    Task<Result<string>> CreateForecastAsync(ForecastDto forecastDto);

    /// <summary>
    /// Updates an existing forecast with new predictions or metadata.
    /// </summary>
    /// <param name="forecastDto">The updated forecast data.</param>
    /// <returns>
    /// A <see cref="Result{T}"/> containing the ID of the updated forecast if successful.
    /// </returns>
    Task<Result<string>> UpdateForecastAsync(ForecastDto forecastDto);

    /// <summary>
    /// Deletes a specific forecast from the system.
    /// </summary>
    /// <param name="id">The unique identifier of the forecast to delete.</param>
    /// <returns>
    /// A <see cref="Result{T}"/> indicating victory if the deletion was successful.
    /// </returns>
    Task<Result<string>> DeleteForecastAsync(string id);
}
