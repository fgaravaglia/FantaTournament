using FantaTournament.Application.DTOs;
using FantaTournament.Domain.Repositories;
using Umbrella.Core;

namespace FantaTournament.Application.Queries;

/// <summary>
/// Implementation of <see cref="IBoardQueries"/> providing access to board data.
/// </summary>
public class BoardQueries : IBoardQueries
{
    private readonly IBoardRepository _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="BoardQueries"/> class.
    /// </summary>
    /// <param name="repository">The repository used to access board data.</param>
    public BoardQueries(IBoardRepository repository)
    {
        _repository = repository;
    }

    /// <inheritdoc/>
    public async Task<Result<IEnumerable<BoardDto>>> SearchBoardsAsync(string name)
    {
        var boards = await _repository.SearchByNameAsync(name);
        
        var dtos = boards.Select(b => new BoardDto 
        { 
            Id = b.Id, 
            Name = b.Name 
        });

        return Result<IEnumerable<BoardDto>>.Success(dtos);
    }

    /// <inheritdoc/>
    public async Task<Result<BoardMatchesDto>> GetBoardMatchesAsync(string boardId)
    {
        var board = await _repository.GetByIdAsync(boardId);
        if (board == null)
        {
            return Result<BoardMatchesDto>.NotFound();
        }

        var matchDtos = board.Matches.Select(m => new MatchDto
        {
            Id = m.Id,
            Code = m.Code,
            Phase = m.Phase.Name,
            Date = m.Date,
            Status = m.Status.DisplayName,
            HomeTeam = m.HomeTeam?.Name ?? m.HomeTeamPlaceholder,
            AwayTeam = m.AwayTeam?.Name ?? m.AwayTeamPlaceholder,
            Result = m.Result
        }).ToList();

        var dto = new BoardMatchesDto
        {
            BoardId = board.Id,
            BoardName = board.Name,
            Matches = matchDtos
        };

        return Result<BoardMatchesDto>.Success(dto);
    }

    /// <inheritdoc/>
    public async Task<Result<IEnumerable<TeamDto>>> GetBoardTeamsAsync(string boardId)
    {
        var board = await _repository.GetByIdAsync(boardId);
        if (board == null)
        {
            return Result<IEnumerable<TeamDto>>.NotFound();
        }

        var teams = board.Matches
            .SelectMany(m => new[] { m.HomeTeam, m.AwayTeam })
            .Where(t => t != null)
            .DistinctBy(t => t!.Id) // Check distinct by ID
            .Select(t => new TeamDto 
            { 
                Id = t!.Id, 
                Name = t.Name 
            });


        return Result<IEnumerable<TeamDto>>.Success(teams);
    }
}
