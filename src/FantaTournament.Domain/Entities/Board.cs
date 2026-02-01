using Umbrella.Core.Domain;

namespace FantaTournament.Domain.Entities;

/// <summary>
/// Represents a specific tournament board (e.g., World Cup 2026).
/// Acts as the Aggregate Root for the tournament data.
/// </summary>
public class Board : Entity
{
    /// <summary>
    /// Gets or sets the name of the tournament (e.g., "FIFA World Cup 2026").
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Gets or sets the unique code identifying the board.
    /// </summary>
    public required string Code { get; set; }

    /// <summary>
    /// Gets or sets the list of matches scheduled for this board.
    /// </summary>
    public List<Match> Matches { get; set; } = [];

    /// <summary>
    /// Gets or sets the list of teams participating in this board.
    /// </summary>
    public List<Team> Teams { get; set; } = [];
}
