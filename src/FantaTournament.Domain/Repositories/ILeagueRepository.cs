using FantaTournament.Domain.Entities;

namespace FantaTournament.Domain.Repositories;

/// <summary>
/// Defines the contract for managing League data.
/// </summary>
public interface ILeagueRepository
{
    /// <summary>
    /// Retrieves a league by its unique identifier.
    /// </summary>
    Task<League?> GetByIdAsync(string id);

    /// <summary>
    /// Retrieves all leagues associated with a specific board.
    /// </summary>
    Task<IEnumerable<League>> GetByBoardIdAsync(string boardId);

    /// <summary>
    /// Persists changes to a league.
    /// </summary>
    Task UpdateAsync(League league);
}
