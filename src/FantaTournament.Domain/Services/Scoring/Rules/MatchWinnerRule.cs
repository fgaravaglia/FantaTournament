using FantaTournament.Domain.Entities;
using FantaTournament.Domain.ValueObjects;

namespace FantaTournament.Domain.Services.Scoring.Rules;

/// <summary>
/// Represents the scoring rule based on predicting the winner of the match correctly.
/// </summary>
/// <remarks>
/// This rule determines the winner based on the home and away goals difference.
/// It considers regular time scores unless extra time scores are available.
/// </remarks>
public class MatchWinnerRule : IScoringRule
{
    public string Code => "MATCH_WINNER";
    public string DisplayName => "Match Winner";
    public string Description => "Pronostico per il vincitore del match; Valido solo per la fase finale del torneo";

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
    /// <returns>3.0 points for Group Stage matches; 5.0 points for other phases if the match winner is correctly predicted. Otherwise, 0.0.</returns>
    public double Calculate(Match match, Prediction prediction)
    {
        if (match.Result == null) return 0.0;

        // Use ExtraTime if available (assuming it represents the final state including penalties/ET), otherwise RegularTime
        var actualFinalScore = match.Result.ExtraTime ?? match.Result.RegularTime;
        var actualSign = Math.Sign(actualFinalScore.HomeGoals - actualFinalScore.AwayGoals);

        var predictedFinalScore = prediction.PredictedResult.ExtraTime ?? prediction.PredictedResult.RegularTime;
        var predictedSign = Math.Sign(predictedFinalScore.HomeGoals - predictedFinalScore.AwayGoals);

        if (actualSign != predictedSign) return 0.0;

        return match.Phase == MatchPhase.GroupStage ? 3.0 : 5.0;
    }
}
