using Umbrella.Core.Domain;

namespace FantaTournament.Domain.Entities;

/// <summary>
/// Represents a private or public league (mini-tournament) within a Board.
/// </summary>
public class League : Entity
{
    /// <summary>
    /// Gets or sets the name of the league.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the ID of the board this league belongs to.
    /// </summary>
    public string BoardId { get; set; } = string.Empty;

    /// <summary>
    /// Gets the list of user IDs who are members of this league.
    /// </summary>
    public List<string> MemberIds { get; private set; } = new();

    /// <summary>
    /// Adds a user to the league.
    /// </summary>
    /// <param name="userId">The ID of the user to add.</param>
    public void AddMember(string userId)
    {
        if (!MemberIds.Contains(userId))
        {
            MemberIds.Add(userId);
        }
    }

    /// <summary>
    /// Removes a user from the league.
    /// </summary>
    /// <param name="userId">The ID of the user to remove.</param>
    public void RemoveMember(string userId)
    {
        if (MemberIds.Contains(userId))
        {
            MemberIds.Remove(userId);
        }
    }
}
