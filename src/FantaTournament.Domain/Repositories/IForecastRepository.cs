using FantaTournament.Domain.Entities;

namespace FantaTournament.Domain.Repositories;

/// <summary>
/// Defines the contract for accessing and managing Forecast data within the domain.
/// </summary>
public interface IForecastRepository
{
    /// <summary>
    /// Retrieves a forecast by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the forecast.</param>
    /// <returns>
    /// The <see cref="Forecast"/> if found; otherwise, <see langword="null"/>.
    /// </returns>
    Task<Forecast?> GetByIdAsync(string id);

    /// <summary>
    /// Retrieves all forecasts associated with a specific user.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <returns>
    /// A collection of <see cref="Forecast"/> entities belonging to the user.
    /// </returns>
    Task<IEnumerable<Forecast>> GetByUserIdAsync(string userId);

    /// <summary>
    /// Persists changes made to an existing forecast.
    /// </summary>
    /// <param name="forecast">The forecast entity containing the changes to be updated.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous update operation.</returns>
    Task UpdateAsync(Forecast forecast);
}
