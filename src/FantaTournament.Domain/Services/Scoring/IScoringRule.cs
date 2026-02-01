using FantaTournament.Domain.Entities;
using FantaTournament.Domain.ValueObjects;

namespace FantaTournament.Domain.Services.Scoring;

/// <summary>
/// Defines a single scoring rule that can be applied to a match prediction.
/// </summary>
public interface IScoringRule
{
    /// <summary>
    /// Gets the unique code of the rule.
    /// </summary>
    string Code { get; }

    /// <summary>
    /// Gets the display name of the rule.
    /// </summary>
    string DisplayName { get; }
    
    /// <summary>
    /// Gets the description of the rule.
    /// </summary>
    string Description { get; }

    /// <summary>
    /// Determines if the rule is applicable to the given match phase.
    /// </summary>
    bool IsApplicable(MatchPhase phase);

    /// <summary>
    /// Calculates the points for a given match and prediction.
    /// </summary>
    /// <returns>The points earned, or 0 if the rule criteria are not met.</returns>
    double Calculate(Match match, Prediction prediction);
}
