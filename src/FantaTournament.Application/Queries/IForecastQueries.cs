using FantaTournament.Application.DTOs;
using Umbrella.Core;

namespace FantaTournament.Application.Queries;

/// <summary>
/// Defines queries related to Forecasts.
/// </summary>
public interface IForecastQueries : IQueryHandler
{
    /// <summary>
    /// Retrieves a forecast by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the forecast.</param>
    /// <returns>
    /// A <see cref="Result{T}"/> containing a <see cref="ForecastDto"/> if found; 
    /// otherwise, returns a Not Found result.
    /// </returns>
    Task<Result<ForecastDto>> GetForecastByIdAsync(string id);

    /// <summary>
    /// Retrieves all forecasts for a specific user.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <returns>
    /// A <see cref="Result{T}"/> containing a collection of <see cref="ForecastDto"/> objects.
    /// Returns an empty collection if the user has no forecasts.
    /// </returns>
    Task<Result<IEnumerable<ForecastDto>>> GetForecastsByUserIdAsync(string userId);
}
