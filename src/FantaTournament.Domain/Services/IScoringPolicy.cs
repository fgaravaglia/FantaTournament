using FantaTournament.Domain.Entities;

namespace FantaTournament.Domain.Services;

/// <summary>
/// Defines the contract for calculating scores for match predictions.
/// </summary>
public interface IScoringPolicy
{
    /// <summary>
    /// Calculates the score for a single prediction based on the actual match result.
    /// </summary>
    /// <param name="match">The actual match with the final result.</param>
    /// <param name="prediction">The user's prediction for the match.</param>
    /// <returns>The calculated score (points) for the prediction.</returns>
    double CalculateScore(Match match, Prediction prediction);
}
