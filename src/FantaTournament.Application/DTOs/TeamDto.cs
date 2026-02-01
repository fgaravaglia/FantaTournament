namespace FantaTournament.Application.DTOs;

/// <summary>
/// Data Transfer Object for a Team.
/// </summary>
public class TeamDto
{
    /// <summary>
    /// Gets or sets the unique identifier of the team.
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the name of the team.
    /// </summary>
    public string Name { get; set; } = string.Empty;
}
