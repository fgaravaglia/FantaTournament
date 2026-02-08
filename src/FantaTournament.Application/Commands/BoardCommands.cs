using FantaTournament.Application.DTOs;
using FantaTournament.Domain.Entities;
using FantaTournament.Domain.Repositories;
using FantaTournament.Domain.ValueObjects;
using FantaTournament.Domain.Events;
using Umbrella.Core;
using Umbrella.Core.Messaging;

namespace FantaTournament.Application.Commands;

/// <summary>
/// Implementation of <see cref="IBoardCommands"/> providing administrative operations for boards.
/// </summary>
public class BoardCommands : IBoardCommands
{
    private readonly IBoardRepository _repository;
    private readonly IEventBus _eventBus;

    /// <summary>
    /// Initializes a new instance of the <see cref="BoardCommands"/> class.
    /// </summary>
    /// <param name="repository">The repository used to persist board changes.</param>
    /// <param name="eventBus">The event bus to publish events.</param>
    public BoardCommands(IBoardRepository repository, IEventBus eventBus)
    {
        _repository = repository;
        _eventBus = eventBus;
    }

    /// <inheritdoc/>
    public async Task<Result<string>> UpdateMatchResultAsync(string boardId, string matchId, MatchResult result)
    {
        var board = await _repository.GetByIdAsync(boardId);
        if (board == null) return Result<string>.NotFound();

        var match = board.Matches.FirstOrDefault(m => m.Id == matchId);
        if (match == null) return Result<string>.Failure(new[] { "Match not found" });

        match.Result = result;

        await _repository.UpdateAsync(board);

        // If Played, notify subscribers
        if (match.Status == MatchStatus.Played)
        {
            await _eventBus.PublishAsync(new MatchResultUpdatedEvent(boardId, matchId, result));
        }

        return Result<string>.Success(match.Id);
    }

    /// <inheritdoc/>
    public async Task<Result<string>> UpdateMatchStatusAsync(string boardId, string matchId, MatchStatus status)
    {
        var board = await _repository.GetByIdAsync(boardId);
        if (board == null) return Result<string>.NotFound();

        var match = board.Matches.FirstOrDefault(m => m.Id == matchId);
        if (match == null) return Result<string>.Failure(new[] { "Match not found" });

        match.Status = status;

        await _repository.UpdateAsync(board);

        // If transitioned to Played or updated while Played, notify subscribers
        if (status == MatchStatus.Played && match.Result != null)
        {
            await _eventBus.PublishAsync(new MatchResultUpdatedEvent(boardId, matchId, match.Result));
        }

        return Result<string>.Success(match.Id);
    }

    /// <inheritdoc/>
    public async Task<Result<string>> ImportMatchesAsync(string boardId, IEnumerable<MatchDto> matchesDtos)
    {
        var board = await _repository.GetByIdAsync(boardId);
        if (board == null) return Result<string>.NotFound();

        foreach (var dto in matchesDtos)
        {
            // Simple mapping - assuming simpler implementation for now.
            // In a real scenario we might match existing teams by Name or Id.
            // Here we assume simple import.
            
            // Map Phase - if string not found, default or error? 
            // We'll trust DTO or do manual mapping. 
            // For simplicity, let's assume valid Phase Names or default.
            
            // NOTE: MatchPhase is a SmartEnum. We need to parse it.
            var phase = MatchPhase.GroupStage; // Default/FallBack
            // Reflection/Parsing logic would be better but keeping it simple for prototype
            if (dto.Phase == MatchPhase.RoundOf16.Name) phase = MatchPhase.RoundOf16;
            else if (dto.Phase == MatchPhase.QuarterFinals.Name) phase = MatchPhase.QuarterFinals;
            else if (dto.Phase == MatchPhase.SemiFinals.Name) phase = MatchPhase.SemiFinals;
            else if (dto.Phase == MatchPhase.Final1_2.Name) phase = MatchPhase.Final1_2;
            else if (dto.Phase == MatchPhase.Final3_4.Name) phase = MatchPhase.Final3_4;


            var match = new Match
            {
                Code = dto.Code,
                Date = dto.Date,
                Phase = phase,
                HomeTeamPlaceholder = dto.HomeTeam, // Use Name as placeholder initially
                AwayTeamPlaceholder = dto.AwayTeam,
                Status = MatchStatus.Scheduled
            };

            // If we have Team Entities, we should resolve them. 
            // But this command might just import the schedule structure.
            
            board.Matches.Add(match);
        }

        await _repository.UpdateAsync(board);
        return Result<string>.Success(board.Id);
    }
}
