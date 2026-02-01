using Umbrella.Core.Domain;

namespace FantaTournament.Domain.Entities;

/// <summary>
/// Represents a football team participating in the tournament.
/// </summary>
public class Team : Entity
{
    /// <summary>
    /// Gets or sets the display name of the team (e.g., "Germany").
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Gets or sets the board-specific code for the team (e.g., "A1").
    /// </summary>
    public required string BoardCode { get; set; }
}
