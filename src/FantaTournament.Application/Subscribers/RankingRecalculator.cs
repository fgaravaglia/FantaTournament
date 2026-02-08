using FantaTournament.Domain.Entities;
using FantaTournament.Domain.Events;
using FantaTournament.Domain.Repositories;
using FantaTournament.Domain.Services;
using Microsoft.Extensions.Logging;
using Umbrella.Core.Messaging;

namespace FantaTournament.Application.Subscribers;

/// <summary>
/// Subscriber that reacts to match result updates by recalculating global and league rankings.
/// </summary>
public class RankingRecalculator : IEventHandler<MatchResultUpdatedEvent>
{
    private readonly IForecastRepository _forecastRepository;
    private readonly ILeagueRepository _leagueRepository;
    private readonly IRankingRepository _rankingRepository;
    private readonly RankingService _rankingService;
    private readonly ILogger<RankingRecalculator> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="RankingRecalculator"/> class.
    /// </summary>
    public RankingRecalculator(
        IForecastRepository forecastRepository,
        ILeagueRepository leagueRepository,
        IRankingRepository rankingRepository,
        RankingService rankingService,
        ILogger<RankingRecalculator> logger)
    {
        _forecastRepository = forecastRepository;
        _leagueRepository = leagueRepository;
        _rankingRepository = rankingRepository;
        _rankingService = rankingService;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task HandleAsync(MatchResultUpdatedEvent @event)
    {
        _logger.LogInformation("Recalculating rankings for board {BoardId} due to match {MatchId} update", 
            @event.BoardId, @event.MatchId);

        try
        {
            var forecasts = (await _forecastRepository.GetByBoardIdAsync(@event.BoardId)).ToList();
            if (!forecasts.Any())
            {
                _logger.LogWarning("No forecasts found for board {BoardId}, skipping ranking recalculation", @event.BoardId);
                return;
            }

            // 1. Recalculate Global Ranking
            _logger.LogDebug("Recalculating Global Ranking for board {BoardId}", @event.BoardId);
            var globalRanking = _rankingService.CalculateGlobalRanking(@event.BoardId, forecasts);
            await _rankingRepository.UpdateAsync(globalRanking);

            // 2. Get all leagues for this board
            var leagues = await _leagueRepository.GetByBoardIdAsync(@event.BoardId);

            // 3. Recalculate League Rankings in parallel
            var tasks = leagues.Select(async league =>
            {
                try
                {
                    _logger.LogDebug("Recalculating Ranking for league {LeagueId} ({LeagueName})", league.Id, league.Name);
                    var leagueRanking = _rankingService.CalculateLeagueRanking(league, forecasts);
                    await _rankingRepository.UpdateAsync(leagueRanking);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error recalculating ranking for league {LeagueId}", league.Id);
                }
            });

            await Task.WhenAll(tasks);

            _logger.LogInformation("Successfully recalculated global ranking and {Count} league rankings for board {BoardId}", 
                leagues.Count(), @event.BoardId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Extreme error during ranking recalculation for board {BoardId}", @event.BoardId);
        }
    }
}
