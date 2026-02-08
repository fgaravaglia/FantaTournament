using FantaTournament.Domain.Entities;
using FantaTournament.Domain.Events;
using FantaTournament.Domain.Repositories;
using FantaTournament.Domain.Services;
using Microsoft.Extensions.Logging;
using Umbrella.Core.Messaging;

namespace FantaTournament.Application.Subscribers;

/// <summary>
/// Subscriber that reacts to match result updates by recalculating all forecasts for the board.
/// </summary>
public class ForecastRecalculator : IEventHandler<MatchResultUpdatedEvent>
{
    private readonly IForecastRepository _forecastRepository;
    private readonly IBoardRepository _boardRepository;
    private readonly IScoringPolicy _scoringPolicy;
    private readonly ILogger<ForecastRecalculator> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="ForecastRecalculator"/> class.
    /// </summary>
    public ForecastRecalculator(
        IForecastRepository forecastRepository,
        IBoardRepository boardRepository,
        IScoringPolicy scoringPolicy,
        ILogger<ForecastRecalculator> logger)
    {
        _forecastRepository = forecastRepository;
        _boardRepository = boardRepository;
        _scoringPolicy = scoringPolicy;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task HandleAsync(MatchResultUpdatedEvent @event)
    {
        _logger.LogInformation("Recalculating forecasts for board {BoardId} due to match {MatchId} update", 
            @event.BoardId, @event.MatchId);

        try
        {
            var board = await _boardRepository.GetByIdAsync(@event.BoardId);
            if (board == null)
            {
                _logger.LogWarning("Board {BoardId} not found for forecast recalculation", @event.BoardId);
                return;
            }

            var forecasts = await _forecastRepository.GetByBoardIdAsync(@event.BoardId);
            
            // Parallel recalculation and update for each forecast
            var tasks = forecasts.Select(async forecast =>
            {
                try
                {
                    _logger.LogDebug("Recalculating score for forecast {ForecastId} (User: {UserId})", forecast.Id, forecast.UserId);
                    forecast.RecalculateScore(_scoringPolicy, board);
                    await _forecastRepository.UpdateAsync(forecast);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error recalculating forecast {ForecastId}", forecast.Id);
                }
            });

            await Task.WhenAll(tasks);
            
            _logger.LogInformation("Successfully recalculated {Count} forecasts for board {BoardId}", 
                forecasts.Count(), @event.BoardId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Extreme error during forecast recalculation for board {BoardId}", @event.BoardId);
        }
    }
}
