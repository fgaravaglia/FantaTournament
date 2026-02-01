using FantaTournament.Domain.ValueObjects;

namespace FantaTournament.Application.DTOs;

/// <summary>
/// Data Transfer Object for a Match.
/// </summary>
public class MatchDto
{
    /// <summary>
    /// Gets or sets the unique identifier of the match.
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the match code.
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the tournament phase name.
    /// </summary>
    public string Phase { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the scheduled date and time.
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Gets or sets the current status of the match.
    /// </summary>
    public string Status { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the home team name or placeholder.
    /// </summary>
    public string HomeTeam { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the away team name or placeholder.
    /// </summary>
    public string AwayTeam { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the final result of the match, if available.
    /// </summary>
    public MatchResult? Result { get; set; }
}
