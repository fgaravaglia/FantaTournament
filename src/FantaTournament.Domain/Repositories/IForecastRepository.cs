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

    Task<IEnumerable<Forecast>> GetByUserIdAsync(string userId);

    /// <summary>
    /// Retrieves all forecasts associated with a specific board.
    /// </summary>
    /// <param name="boardId">The unique identifier of the board.</param>
    /// <returns>
    /// A collection of <see cref="Forecast"/> entities for the board.
    /// </returns>
    Task<IEnumerable<Forecast>> GetByBoardIdAsync(string boardId);

    /// <summary>
    /// Persists changes made to an existing forecast.
    /// </summary>
    /// <param name="forecast">The forecast entity containing the changes to be updated.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous update operation.</returns>
    Task UpdateAsync(Forecast forecast);

    /// <summary>
    /// Removes a forecast from the repository.
    /// </summary>
    /// <param name="id">The unique identifier of the forecast to remove.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous deletion operation.</returns>
    Task DeleteAsync(string id);
}
