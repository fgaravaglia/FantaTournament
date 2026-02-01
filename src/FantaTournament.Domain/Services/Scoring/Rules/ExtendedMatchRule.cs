using FantaTournament.Domain.Entities;
using FantaTournament.Domain.ValueObjects;

namespace FantaTournament.Domain.Services.Scoring.Rules;

/// <summary>
/// Represents the scoring rule based on the goal difference in extra time (or penalties).
/// </summary>
/// <remarks>
/// This rule applies only to non-Group Stage matches and awards points if the predicted goal difference matches the actual goal difference in extra time.
/// </remarks>
public class ExtendedMatchRule : IScoringRule
{
    public string Code => "EXTENDED_SCORE";
    public string DisplayName => "Extended Score";
    public string Description => "Pronostico per il risultato al termine dei tempi supplementari/Rigori; Valido solo per la fase finale del torneo";

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
    /// <returns>1.5 points if the goal difference in extra time is correct. Otherwise, 0.0.</returns>
    public double Calculate(Match match, Prediction prediction)
    {
        if (match.Result?.ExtraTime == null || prediction.PredictedResult.ExtraTime == null) return 0.0;

        var actualDiff = match.Result.ExtraTime.HomeGoals - match.Result.ExtraTime.AwayGoals;
        var predictedDiff = prediction.PredictedResult.ExtraTime.HomeGoals - prediction.PredictedResult.ExtraTime.AwayGoals;

        if (actualDiff == predictedDiff)
        {
            return 1.5;
        }

        return 0.0;
    }
}
