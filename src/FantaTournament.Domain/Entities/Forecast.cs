using FantaTournament.Domain.Services;
using FantaTournament.Domain.ValueObjects;
using Umbrella.Core.Domain;

namespace FantaTournament.Domain.Entities;

/// <summary>
/// Represents a user's complete set of predictions for a specific tournament.
/// </summary>
public class Forecast : AuditableEntity
{
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
    public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the list of predictions made by the user for matches in the board.
    /// </summary>
    public List<Prediction> Predictions { get; set; } = [];

    /// <summary>
    /// recalculates the total score of the forecast based on the provided scoring policy and board state.
    /// </summary>
    public void RecalculateScore(IScoringPolicy policy, Board board)
    {
        TotalScore = 0;
        foreach (var prediction in Predictions)
        {
            var match = board.Matches.FirstOrDefault(m => m.Id == prediction.MatchId);
            
            // Only score matches that are effectively finished
            if (match is not null && match.Status == MatchStatus.Played && match.Result is not null)
            {
                prediction.Score = policy.CalculateScore(match, prediction);
                TotalScore += prediction.Score;
            }
            else
            {
                prediction.Score = 0;
            }
        }
    }
}
