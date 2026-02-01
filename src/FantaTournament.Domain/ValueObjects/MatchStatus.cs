using Umbrella.Core.Domain;

namespace FantaTournament.Domain.ValueObjects;

/// <summary>
/// Represents the current status of a match.
/// </summary>
public class MatchStatus : ValueObject
{
    /// <summary>
    /// Gets the unique code of the status.
    /// </summary>
    public string Code { get; }
    
    /// <summary>
    /// Gets the display name of the status.
    /// </summary>
    public string DisplayName { get; }

    private MatchStatus(string code, string displayName)
    {
        Code = code;
        DisplayName = displayName;
    }

    /// <summary>
    /// The match is scheduled but has not started yet.
    /// </summary>
    public static readonly MatchStatus Scheduled = new("SCHEDULED", "Scheduled");
    
    /// <summary>
    /// The match is currently being played.
    /// </summary>
    public static readonly MatchStatus InProgress = new("IN_PROGRESS", "In Progress");
    
    /// <summary>
    /// The match has finished and the result is final.
    /// </summary>
    public static readonly MatchStatus Played = new("PLAYED", "Played");

    /// <summary>
    /// Checks if the match is finished.
    /// </summary>
    public bool IsFinished() => this.Equals(Played);

    /// <inheritdoc/>
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Code;
    }
}
