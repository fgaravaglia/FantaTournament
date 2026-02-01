using Umbrella.Core.Domain;

namespace FantaTournament.Domain.Entities;

/// <summary>
/// Represents a calculated ranking (leaderboard) for a specific scope (Global or League).
/// </summary>
public class Ranking : Entity
{
    /// <summary>
    /// Gets or sets the ID of the board this ranking belongs to.
    /// </summary>
    public string BoardId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the ID of the league this ranking belongs to. Null if it is a Global ranking.
    /// </summary>
    public string? LeagueId { get; set; }

    /// <summary>
    /// Gets or sets the list of ordered entries in the ranking.
    /// </summary>
    public List<RankingEntry> Entries { get; set; } = new();

    /// <summary>
    /// Gets or sets the timestamp when this ranking was last calculated/updated.
    /// </summary>
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
