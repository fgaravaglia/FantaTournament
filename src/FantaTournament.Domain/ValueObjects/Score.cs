namespace FantaTournament.Domain.ValueObjects;

/// <summary>
/// Represents the score of a match or a specific period of a match (e.g., regular time, extra time).
/// </summary>
/// <param name="HomeGoals">The number of goals scored by the home team.</param>
/// <param name="AwayGoals">The number of goals scored by the away team.</param>
public record Score(int HomeGoals, int AwayGoals);
