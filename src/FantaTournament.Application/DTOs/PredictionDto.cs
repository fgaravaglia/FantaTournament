namespace FantaTournament.Application.DTOs;

/// <summary>
/// Represents a prediction for a match in a DTO format.
/// </summary>
public class PredictionDto
{
    /// <summary>
    /// Gets or sets the unique identifier of the match.
    /// </summary>
    public required string MatchId { get; set; }

    /// <summary>
    /// Gets or sets the predicted home score (Regular Time).
    /// </summary>
    public int PredictedHomeScore { get; set; }

    /// <summary>
    /// Gets or sets the predicted away score (Regular Time).
    /// </summary>
    public int PredictedAwayScore { get; set; }

    /// <summary>
    /// Gets or sets the predicted home score (Extra Time/Penalties), if applicable.
    /// </summary>
    public int? PredictedExtraHomeScore { get; set; }

    /// <summary>
    /// Gets or sets the predicted away score (Extra Time/Penalties), if applicable.
    /// </summary>
    public int? PredictedExtraAwayScore { get; set; }

    /// <summary>
    /// Gets or sets the score earned for this prediction.
    /// </summary>
    public double Score { get; set; }
}
