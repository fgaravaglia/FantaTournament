namespace Umbrella.Core.Messaging;

/// <summary>
/// Marker interface for all events in the system.
/// </summary>
public interface IEvent
{
    /// <summary>
    /// Gets the date and time when the event occurred.
    /// </summary>
    DateTime OccurredOn { get; }
}
