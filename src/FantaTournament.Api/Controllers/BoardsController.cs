using FantaTournament.Application.Commands;
using FantaTournament.Application.DTOs;
using FantaTournament.Application.Queries;
using FantaTournament.Domain.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FantaTournament.Api.Controllers;

/// <summary>
/// Controller for managing and querying Boards.
/// </summary>
public class BoardsController : ApiControllerBase
{
    private readonly IBoardQueries _boardQueries;
    private readonly IBoardCommands _boardCommands;

    public BoardsController(IBoardQueries boardQueries, IBoardCommands boardCommands)
    {
        _boardQueries = boardQueries;
        _boardCommands = boardCommands;
    }

    /// <summary>
    /// Searches for boards by name.
    /// </summary>
    /// <param name="name">The name or part of the name to search for.</param>
    /// <returns>A collection of matching boards.</returns>
    [HttpGet]
    [Authorize("BoardReader")]
    public async Task<ActionResult<IEnumerable<BoardDto>>> SearchBoards([FromQuery] string name = "")
    {
        var result = await _boardQueries.SearchBoardsAsync(name);
        return MapToActionResult(result);
    }

    /// <summary>
    /// Retrieves all matches for a specific board.
    /// </summary>
    /// <param name="boardId">The unique identifier of the board.</param>
    /// <returns>The matches of the board.</returns>
    [HttpGet("{boardId}/matches")]
    [Authorize("BoardReader")]
    public async Task<ActionResult<BoardMatchesDto>> GetBoardMatches(string boardId)
    {
        var result = await _boardQueries.GetBoardMatchesAsync(boardId);
        return MapToActionResult(result);
    }

    /// <summary>
    /// Retrieves all teams participating in a specific board.
    /// </summary>
    /// <param name="boardId">The unique identifier of the board.</param>
    /// <returns>The teams participating in the board.</returns>
    [HttpGet("{boardId}/teams")]
    [Authorize("BoardReader")]
    public async Task<ActionResult<IEnumerable<TeamDto>>> GetBoardTeams(string boardId)
    {
        var result = await _boardQueries.GetBoardTeamsAsync(boardId);
        return MapToActionResult(result);
    }

    /// <summary>
    /// Updates the result of a specific match.
    /// </summary>
    /// <param name="boardId">The ID of the board.</param>
    /// <param name="matchId">The ID of the match.</param>
    /// <param name="result">The new result.</param>
    /// <returns>The ID of the updated match.</returns>
    [HttpPatch("{boardId}/matches/{matchId}/result")]
    [Authorize("BoardWriter")]
    public async Task<ActionResult<string>> UpdateMatchResult(string boardId, string matchId, [FromBody] MatchResult result)
    {
        var commandResult = await _boardCommands.UpdateMatchResultAsync(boardId, matchId, result);
        return MapToActionResult(commandResult);
    }

    /// <summary>
    /// Updates the status of a specific match.
    /// </summary>
    /// <param name="boardId">The ID of the board.</param>
    /// <param name="matchId">The ID of the match.</param>
    /// <param name="status">The new status.</param>
    /// <returns>The ID of the updated match.</returns>
    [HttpPatch("{boardId}/matches/{matchId}/status")]
    [Authorize("BoardWriter")]
    public async Task<ActionResult<string>> UpdateMatchStatus(string boardId, string matchId, [FromBody] MatchStatus status)
    {
        var commandResult = await _boardCommands.UpdateMatchStatusAsync(boardId, matchId, status);
        return MapToActionResult(commandResult);
    }

    /// <summary>
    /// Imports matches into a board.
    /// </summary>
    /// <param name="boardId">The ID of the board.</param>
    /// <param name="matches">The matches to import.</param>
    /// <returns>The ID of the board.</returns>
    [HttpPost("{boardId}/matches/import")]
    [Authorize("BoardWriter")]
    public async Task<ActionResult<string>> ImportMatches(string boardId, [FromBody] IEnumerable<MatchDto> matches)
    {
        var commandResult = await _boardCommands.ImportMatchesAsync(boardId, matches);
        return MapToActionResult(commandResult);
    }
}
