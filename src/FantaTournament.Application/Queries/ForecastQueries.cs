using FantaTournament.Application.DTOs;
using FantaTournament.Domain.Repositories;
using Umbrella.Core;

namespace FantaTournament.Application.Queries;

/// <summary>
/// Implementation of <see cref="IForecastQueries"/> providing access to forecast data.
/// </summary>
public class ForecastQueries : IForecastQueries
{
    private readonly IForecastRepository _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="ForecastQueries"/> class.
    /// </summary>
    /// <param name="repository">The repository used to access forecast data.</param>
    public ForecastQueries(IForecastRepository repository)
    {
        _repository = repository;
    }

    /// <inheritdoc/>
    public async Task<Result<ForecastDto>> GetForecastByIdAsync(string id)
    {
        var forecast = await _repository.GetByIdAsync(id);
        if (forecast == null)
        {
            return Result<ForecastDto>.NotFound();
        }

        return Result<ForecastDto>.Success(MapToDto(forecast));
    }

    /// <inheritdoc/>
    public async Task<Result<IEnumerable<ForecastDto>>> GetForecastsByUserIdAsync(string userId)
    {
        var forecasts = await _repository.GetByUserIdAsync(userId);
        var dtos = forecasts.Select(MapToDto);
        return Result<IEnumerable<ForecastDto>>.Success(dtos);
    }

    private static ForecastDto MapToDto(FantaTournament.Domain.Entities.Forecast forecast)
    {
        return new ForecastDto
        {
            Id = forecast.Id,
            UserId = forecast.UserId,
            BoardId = forecast.BoardId,
            TotalScore = forecast.TotalScore,
            LastUpdatedAt = forecast.LastUpdatedAt,
            Predictions = forecast.Predictions.Select(p => new PredictionDto
            {
                MatchId = p.MatchId,
                PredictedHomeScore = p.PredictedResult.RegularTime.HomeGoals,
                PredictedAwayScore = p.PredictedResult.RegularTime.AwayGoals,
                PredictedExtraHomeScore = p.PredictedResult.ExtraTime?.HomeGoals,
                PredictedExtraAwayScore = p.PredictedResult.ExtraTime?.AwayGoals,
                Score = p.Score
            }).ToList()
        };
    }
}
