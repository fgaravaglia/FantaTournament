using FantaTournament.Domain.Entities;

namespace FantaTournament.Domain.Repositories;

/// <summary>
/// Defines the contract for accessing team data.
/// </summary>
public interface ITeamRepository
{
    /// <summary>
    /// Retrieves all teams participating in a specific board.
    /// </summary>
    /// <param name="boardId">The unique identifier of the board.</param>
    /// <returns>
    /// A collection of <see cref="Team"/> entities for the specified board.
    /// </returns>
    Task<IEnumerable<Team>> GetByBoardIdAsync(string boardId);
}
