using FantaTournament.Domain.ValueObjects;
using Umbrella.Core.Domain;

namespace FantaTournament.Domain.Entities;

/// <summary>
/// Represents a specific match in the tournament.
/// </summary>
public class Match : Entity
{
    /// <summary>
    /// Gets or sets the unique code identifying the match (e.g., from CSV source).
    /// </summary>
    public required string Code { get; set; }

    /// <summary>
    /// Gets or sets the phase of the tournament this match belongs to.
    /// </summary>
    public required MatchPhase Phase { get; set; }

    /// <summary>
    /// Gets or sets the scheduled date and time of the match.
    /// </summary>
    public required DateTime Date { get; set; }
    
    /// <summary>
    /// Gets or sets the current status of the match.
    /// </summary>
    public MatchStatus Status { get; set; } = MatchStatus.Scheduled;
    
    /// <summary>
    /// Gets or sets the ID of the home team, if determined.
    /// </summary>
    public string? HomeTeamId { get; set; }

    /// <summary>
    /// Gets or sets the home team entity.
    /// </summary>
    public Team? HomeTeam { get; set; }

    /// <summary>
    /// Gets or sets the ID of the away team, if determined.
    /// </summary>
    public string? AwayTeamId { get; set; }

    /// <summary>
    /// Gets or sets the away team entity.
    /// </summary>
    public Team? AwayTeam { get; set; }
    
    /// <summary>
    /// Gets or sets the placeholder description for the home team.
    /// </summary>
    public required string HomeTeamPlaceholder { get; set; }

    /// <summary>
    /// Gets or sets the placeholder description for the away team.
    /// </summary>
    public required string AwayTeamPlaceholder { get; set; }
    
    /// <summary>
    /// Gets or sets the actual result of the match.
    /// </summary>
    public MatchResult? Result { get; set; }
}
