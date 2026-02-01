namespace FantaTournament.Application.DTOs;

/// <summary>
/// Data Transfer Object containing a Board and its Matches.
/// </summary>
public class BoardMatchesDto
{
    /// <summary>
    /// Gets or sets the unique identifier of the board.
    /// </summary>
    public string BoardId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the name of the board.
    /// </summary>
    public string BoardName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the list of matches associated with the board.
    /// </summary>
    public List<MatchDto> Matches { get; set; } = new();
}
