using FantaTournament.Domain.Entities;
using FantaTournament.Domain.ValueObjects;

namespace FantaTournament.Domain.Services.Scoring.Rules;

/// <summary>
/// Represents the scoring rule based on the goal difference in regular time.
/// </summary>
/// <remarks>
/// This rule applies only to non-Group Stage matches and awards points if the predicted goal difference matches the actual goal difference in regular time.
/// </remarks>
public class RegularMatchRule : IScoringRule
{
    public string Code => "REGULAR_SCORE";
    public string DisplayName => "Regular Score";
    public string Description => "Pronostico per il risultato al termine dei tempi regolamentari";

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="phase">The phase of the match.</param>
    /// <returns><see langword="true"/> if the phase is not the Group Stage; otherwise, <see langword="false"/>.</returns>
    public bool IsApplicable(MatchPhase phase) => phase != MatchPhase.GroupStage;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="match"><inheritdoc/></param>
    /// <param name="prediction"><inheritdoc/></param>
    /// <returns>1.5 points if the goal difference in regular time is correct. Otherwise, 0.0.</returns>
    public double Calculate(Match match, Prediction prediction)
    {
        if (match.Result == null) return 0.0;
        
        var actualDiff = match.Result.RegularTime.HomeGoals - match.Result.RegularTime.AwayGoals;
        var predictedDiff = prediction.PredictedResult.RegularTime.HomeGoals - prediction.PredictedResult.RegularTime.AwayGoals;

        if (actualDiff == predictedDiff)
        {
            return 1.5;
        }

        return 0.0;
    }
}
