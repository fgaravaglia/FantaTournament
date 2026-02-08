using FantaTournament.Application.DTOs;
using FantaTournament.Domain.Entities;
using FantaTournament.Domain.Repositories;
using FantaTournament.Domain.ValueObjects;
using Umbrella.Core;

namespace FantaTournament.Application.Commands;

/// <summary>
/// Implementation of <see cref="IForecastCommands"/> for managing user forecasts.
/// </summary>
public class ForecastCommands : IForecastCommands
{
    private readonly IForecastRepository _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="ForecastCommands"/> class.
    /// </summary>
    /// <param name="repository">The repository used to persist forecast changes.</param>
    public ForecastCommands(IForecastRepository repository)
    {
        _repository = repository;
    }

    /// <inheritdoc/>
    public async Task<Result<string>> CreateForecastAsync(ForecastDto forecastDto)
    {
        var forecast = new Forecast
        {
            Id = string.IsNullOrWhiteSpace(forecastDto.Id) ? Guid.NewGuid().ToString() : forecastDto.Id,
            UserId = forecastDto.UserId,
            BoardId = forecastDto.BoardId,
            Predictions = forecastDto.Predictions.Select(p => new Prediction
            {
                MatchId = p.MatchId,
                PredictedResult = new MatchResult(
                    new Score(p.PredictedHomeScore, p.PredictedAwayScore),
                    p.PredictedExtraHomeScore.HasValue && p.PredictedExtraAwayScore.HasValue
                        ? new Score(p.PredictedExtraHomeScore.Value, p.PredictedExtraAwayScore.Value)
                        : null)
            }).ToList()
        };

        await _repository.UpdateAsync(forecast);
        return Result<string>.Success(forecast.Id);
    }

    /// <inheritdoc/>
    public async Task<Result<string>> UpdateForecastAsync(ForecastDto forecastDto)
    {
        var forecast = await _repository.GetByIdAsync(forecastDto.Id);
        if (forecast == null) return Result<string>.NotFound();

        forecast.UserId = forecastDto.UserId;
        forecast.BoardId = forecastDto.BoardId;
        forecast.LastUpdatedAt = DateTime.UtcNow;
        forecast.Predictions = forecastDto.Predictions.Select(p => new Prediction
        {
            MatchId = p.MatchId,
            PredictedResult = new MatchResult(
                new Score(p.PredictedHomeScore, p.PredictedAwayScore),
                p.PredictedExtraHomeScore.HasValue && p.PredictedExtraAwayScore.HasValue
                    ? new Score(p.PredictedExtraHomeScore.Value, p.PredictedExtraAwayScore.Value)
                    : null)
        }).ToList();

        await _repository.UpdateAsync(forecast);
        return Result<string>.Success(forecast.Id);
    }

    /// <inheritdoc/>
    public async Task<Result<string>> DeleteForecastAsync(string id)
    {
        var forecast = await _repository.GetByIdAsync(id);
        if (forecast == null) return Result<string>.NotFound();

        await _repository.DeleteAsync(id);
        return Result<string>.Success(id);
    }
}
