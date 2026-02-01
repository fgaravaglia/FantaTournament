using FantaTournament.Domain.Entities;
using FantaTournament.Domain.ValueObjects;

namespace FantaTournament.Domain.Services.Scoring.Rules;

/// <summary>
/// Represents the scoring rule where the predicted result matches the actual result exactly.
/// </summary>
/// <remarks>
/// This rule assigns 3 points for matches in the Group Stage and 5 points for matches in other phases.
/// </remarks>
public class ExactMatchRule : IScoringRule
{
    public string Code => "EXACT_MATCH";
    public string DisplayName => "Exact Match";
    public string Description => "The predicted result must be exactly the same as the actual result.";

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="phase"><inheritdoc/></param>
    /// <returns><inheritdoc/></returns>
    public bool IsApplicable(MatchPhase phase) => true;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="match"><inheritdoc/></param>
    /// <param name="prediction"><inheritdoc/></param>
    /// <returns>3.0 points for Group Stage matches; 5.0 points for other phases if the result is exactly correct. Otherwise, 0.0.</returns>
    public double Calculate(Match match, Prediction prediction)
    {
        if (match.Result == null) return 0.0;
        
        bool isExactMatch = match.Result.Equals(prediction.PredictedResult);

        if (!isExactMatch) return 0.0;

        return match.Phase == MatchPhase.GroupStage ? 3.0 : 5.0;
    }
}
