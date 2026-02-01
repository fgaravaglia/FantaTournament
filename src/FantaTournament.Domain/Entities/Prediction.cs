using FantaTournament.Domain.ValueObjects;

namespace FantaTournament.Domain.Entities;

/// <summary>
/// Represents a user's prediction for the result of a specific match.
/// </summary>
public class Prediction
{
    /// <summary>
    /// Gets or sets the unique identifier of the match being predicted.
    /// </summary>
    public required string MatchId { get; set; }

    /// <summary>
    /// Gets or sets the predicted result of the match.
    /// </summary>
    public required MatchResult PredictedResult { get; set; }

    /// <summary>
    /// Gets or sets the score earned for this prediction.
    /// </summary>
    public double Score { get; set; }
}
