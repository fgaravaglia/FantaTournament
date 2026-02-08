using Umbrella.Core.Messaging;
using FantaTournament.Domain.ValueObjects;

namespace FantaTournament.Domain.Events;

/// <summary>
/// Event published when a match result is updated and status is set to Played.
/// </summary>
public class MatchResultUpdatedEvent : IEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MatchResultUpdatedEvent"/> class.
    /// </summary>
    public MatchResultUpdatedEvent(string boardId, string matchId, MatchResult result)
    {
        BoardId = boardId;
        MatchId = matchId;
        Result = result;
        OccurredOn = DateTime.UtcNow;
    }

    /// <summary>
    /// Gets the unique identifier of the board.
    /// </summary>
    public string BoardId { get; }

    /// <summary>
    /// Gets the unique identifier of the match.
    /// </summary>
    public string MatchId { get; }

    /// <summary>
    /// Gets the new result of the match.
    /// </summary>
    public MatchResult Result { get; }

    /// <inheritdoc/>
    public DateTime OccurredOn { get; }
}
