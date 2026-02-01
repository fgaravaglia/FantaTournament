namespace FantaTournament.Domain.Entities;

/// <summary>
/// Represents a user's entry in the tournament ranking.
/// </summary>
/// <param name="UserId">The unique identifier of the user.</param>
/// <param name="TotalScore">The total score accumulated by the user.</param>
/// <param name="Position">The user's rank position (1st, 2nd, etc.).</param>
public record RankingEntry(string UserId, double TotalScore, int Position);
