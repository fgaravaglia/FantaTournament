namespace FantaTournament.Domain.ValueObjects;

/// <summary>
/// Represents the full result of a match, including regular time and potential extra time/penalties.
/// </summary>
/// <param name="RegularTime">The score at the end of regular time (90 minutes).</param>
/// <param name="ExtraTime">The score at the end of extra time or penalties, if applicable. Can be <see langword="null"/> if the match ended in regular time.</param>
public record MatchResult(Score RegularTime, Score? ExtraTime = null);
