using FantaTournament.Domain.Entities;

namespace FantaTournament.Domain.Services.Scoring;

/// <summary>
/// Service responsible for calculating the total score of a prediction by applying all registered scoring rules.
/// </summary>
/// <remarks>
/// This service iterates through all provided implementations of <see cref="IScoringRule"/>.
/// </remarks>
public class ScoringService : IScoringPolicy
{
    private readonly IEnumerable<IScoringRule> _rules;

    /// <summary>
    /// Initializes a new instance of the <see cref="ScoringService"/> class.
    /// </summary>
    /// <param name="rules">The collection of scoring rules to apply.</param>
    public ScoringService(IEnumerable<IScoringRule> rules)
    {
        _rules = rules;
    }

    /// <inheritdoc/>
    public double CalculateScore(Match match, Prediction prediction)
    {
        double totalScore = 0.0;
        foreach (var rule in _rules)
        {
            if (rule.IsApplicable(match.Phase))
            {
                totalScore += rule.Calculate(match, prediction);
            }
        }
        return totalScore;
    }
}
