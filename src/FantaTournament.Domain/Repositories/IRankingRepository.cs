using FantaTournament.Domain.Entities;

namespace FantaTournament.Domain.Repositories;

/// <summary>
/// Defines the contract for managing Ranking data.
/// </summary>
public interface IRankingRepository
{
    /// <summary>
    /// Retrieves the ranking for a specific board and optional league.
    /// </summary>
    Task<Ranking?> GetAsync(string boardId, string? leagueId = null);

    /// <summary>
    /// Persists or updates a ranking.
    /// </summary>
    Task UpdateAsync(Ranking ranking);
}
