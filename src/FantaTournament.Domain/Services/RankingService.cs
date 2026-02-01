using FantaTournament.Domain.Entities;

namespace FantaTournament.Domain.Services;

/// <summary>
/// Service responsible for calculating rankings based on forecasts.
/// </summary>
public class RankingService
{
    /// <summary>
    /// Calculates the global ranking for a board.
    /// </summary>
    /// <param name="boardId">The ID of the board.</param>
    /// <param name="forecasts">All forecasts for the board.</param>
    /// <returns>A new <see cref="Ranking"/> object containing the global leaderboard.</returns>
    public Ranking CalculateGlobalRanking(string boardId, IEnumerable<Forecast> forecasts)
    {
        var entries = CalculateEntries(forecasts);
        
        return new Ranking
        {
            BoardId = boardId,
            LeagueId = null,
            Entries = entries,
            UpdatedAt = DateTime.UtcNow
        };
    }

    /// <summary>
    /// Calculates the ranking for a specific league.
    /// </summary>
    /// <param name="league">The league to calculate ranking for.</param>
    /// <param name="forecasts">All forecasts (will be filtered by league members).</param>
    /// <returns>A new <see cref="Ranking"/> object strictly for the league members.</returns>
    public Ranking CalculateLeagueRanking(League league, IEnumerable<Forecast> forecasts)
    {
        // Filter forecasts to only include league members
        var leagueForecasts = forecasts.Where(f => league.MemberIds.Contains(f.UserId));
        
        var entries = CalculateEntries(leagueForecasts);

        return new Ranking
        {
            BoardId = league.BoardId, // Assuming league belongs to same board
            LeagueId = league.Id,
            Entries = entries,
            UpdatedAt = DateTime.UtcNow
        };
    }

    private List<RankingEntry> CalculateEntries(IEnumerable<Forecast> forecasts)
    {
        // Sort by TotalScore (Descending)
        // Tie-breaker: LastUpdatedAt (Ascending) -> First come, first served.
        var sortedForecasts = forecasts
            .OrderByDescending(f => f.TotalScore)
            .ThenBy(f => f.LastUpdatedAt)
            .ToList();

        var entries = sortedForecasts
            .Select((f, index) => new RankingEntry(f.UserId, f.TotalScore, index + 1))
            .ToList();

        return entries;
    }
}
