using FantaTournament.Application.DTOs;
using FantaTournament.Domain.ValueObjects;
using Umbrella.Core;

namespace FantaTournament.Application.Commands;

/// <summary>
/// Defines commands for managing Boards, including results, status updates, and structure importing.
/// </summary>
public interface IBoardCommands : ICommandHandler
{
    /// <summary>
    /// Updates the result of a specific match within a board.
    /// </summary>
    /// <param name="boardId">The unique identifier of the board containing the match.</param>
    /// <param name="matchId">The unique identifier of the match to update.</param>
    /// <param name="result">The new match result to apply.</param>
    /// <returns>
    /// A <see cref="Result{T}"/> containing the match ID if the operation succeeded; 
    /// otherwise, returns an error or Not Found result.
    /// </returns>
    Task<Result<string>> UpdateMatchResultAsync(string boardId, string matchId, MatchResult result);

    /// <summary>
    /// Updates the current status of a specific match within a board.
    /// </summary>
    /// <param name="boardId">The unique identifier of the board containing the match.</param>
    /// <param name="matchId">The unique identifier of the match to update.</param>
    /// <param name="status">The new status to set for the match.</param>
    /// <returns>
    /// A <see cref="Result{T}"/> containing the match ID if the operation succeeded; 
    /// otherwise, returns an error or Not Found result.
    /// </returns>
    Task<Result<string>> UpdateMatchStatusAsync(string boardId, string matchId, MatchStatus status);

    /// <summary>
    /// Imports a collection of match definitions into a specific board.
    /// </summary>
    /// <param name="boardId">The unique identifier of the board where matches will be imported.</param>
    /// <param name="matches">A collection of <see cref="MatchDto"/> objects defining the matches to import.</param>
    /// <returns>
    /// A <see cref="Result{T}"/> containing the board ID if the operation succeeded; 
    /// otherwise, returns a Not Found result.
    /// </returns>
    Task<Result<string>> ImportMatchesAsync(string boardId, IEnumerable<MatchDto> matches);
}
