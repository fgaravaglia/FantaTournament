namespace FantaTournament.Application.DTOs;

/// <summary>
/// Data Transfer Object for a Board.
/// </summary>
public class BoardDto
{
    /// <summary>
    /// Gets or sets the unique identifier of the board.
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the name of the tournament represented by the board.
    /// </summary>
    public string Name { get; set; } = string.Empty;
}
