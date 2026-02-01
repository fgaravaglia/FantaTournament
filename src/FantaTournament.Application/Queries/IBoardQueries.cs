using FantaTournament.Application.DTOs;
using Umbrella.Core;

namespace FantaTournament.Application.Queries;

/// <summary>
/// Defines queries related to Boards.
/// </summary>
public interface IBoardQueries : IQueryHandler
{
    /// <summary>
    /// Searches for boards by name.
    /// </summary>
    /// <param name="name">The name or part of the name to search for.</param>
    /// <returns>
    /// A <see cref="Result{T}"/> containing a collection of matching <see cref="BoardDto"/> objects.
    /// Returns an empty collection if no matches are found.
    /// </returns>
    Task<Result<IEnumerable<BoardDto>>> SearchBoardsAsync(string name);

    /// <summary>
    /// Retrieves all matches for a specific board, including results and team details.
    /// </summary>
    /// <param name="boardId">The unique identifier of the board.</param>
    /// <returns>
    /// A <see cref="Result{T}"/> containing a <see cref="BoardMatchesDto"/> if the board exists; 
    /// otherwise, returns a Not Found result.
    /// </returns>
    Task<Result<BoardMatchesDto>> GetBoardMatchesAsync(string boardId);

    /// <summary>
    /// Retrieves all distinct teams participating in a specific board.
    /// </summary>
    /// <param name="boardId">The unique identifier of the board.</param>
    /// <returns>
    /// A <see cref="Result{T}"/> containing a collection of <see cref="TeamDto"/> objects if the board exists; 
    /// otherwise, returns a Not Found result.
    /// </returns>
    Task<Result<IEnumerable<TeamDto>>> GetBoardTeamsAsync(string boardId);
}
