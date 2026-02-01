namespace FantaTournament.Application.DTOs;

/// <summary>
/// Represents a user's forecast in a DTO format.
/// </summary>
public class ForecastDto
{
    /// <summary>
    /// Gets or sets the unique identifier of the forecast.
    /// </summary>
    public required string Id { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the user who owns this forecast.
    /// </summary>
    public required string UserId { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the board this forecast applies to.
    /// </summary>
    public required string BoardId { get; set; }

    /// <summary>
    /// Gets or sets the total score accumulated by this forecast.
    /// </summary>
    public double TotalScore { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the forecast was last updated.
    /// </summary>
    public DateTime LastUpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the list of predictions made by the user.
    /// </summary>
    public List<PredictionDto> Predictions { get; set; } = [];
}
