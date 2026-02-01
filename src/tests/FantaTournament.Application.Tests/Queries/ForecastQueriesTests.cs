using FantaTournament.Application.DTOs;
using FantaTournament.Application.Queries;
using FantaTournament.Domain.Entities;
using FantaTournament.Domain.Repositories;
using FantaTournament.Domain.ValueObjects;
using NSubstitute;
using NUnit.Framework;

namespace FantaTournament.Application.Tests.Queries;

[TestFixture]
public class ForecastQueriesTests
{
    private IForecastRepository _repository;
    private ForecastQueries _queries;

    [SetUp]
    public void SetUp()
    {
        _repository = Substitute.For<IForecastRepository>();
        _queries = new ForecastQueries(_repository);
    }

    [Test]
    public async Task GetForecastByIdAsync_WhenForecastExists_ShouldReturnSuccessWithDto()
    {
        // Arrange
        var forecast = new Forecast 
        { 
            UserId = "user-1", 
            BoardId = "board-1",
            TotalScore = 10.5,
            Predictions = new List<Prediction>
            {
                new Prediction 
                { 
                    MatchId = "match-1", 
                    PredictedResult = new MatchResult(new Score(2, 1)),
                    Score = 3
                }
            }
        };
        _repository.GetByIdAsync("f-1").Returns(forecast);

        // Act
        var result = await _queries.GetForecastByIdAsync("f-1");

        // Assert
        Assert.That(result.Succeeded, Is.True);
        Assert.That(result.Data, Is.Not.Null);
        Assert.That(result.Data!.UserId, Is.EqualTo("user-1"));
        Assert.That(result.Data.TotalScore, Is.EqualTo(10.5));
        Assert.That(result.Data.Predictions.Count, Is.EqualTo(1));
        Assert.That(result.Data.Predictions[0].PredictedHomeScore, Is.EqualTo(2));
        Assert.That(result.Data.Predictions[0].PredictedAwayScore, Is.EqualTo(1));
    }

    [Test]
    public async Task GetForecastByIdAsync_WhenForecastDoesNotExist_ShouldReturnNotFound()
    {
        // Arrange
        _repository.GetByIdAsync("f-1").Returns((Forecast?)null);

        // Act
        var result = await _queries.GetForecastByIdAsync("f-1");

        // Assert
        Assert.That(result.Succeeded, Is.False);
        Assert.That(result.Errors, Contains.Item("Not Found"));
    }

    [Test]
    public async Task GetForecastsByUserIdAsync_ShouldReturnSuccessWithDtos()
    {
        // Arrange
        var forecasts = new List<Forecast>
        {
            new Forecast { UserId = "user-1", BoardId = "board-1" },
            new Forecast { UserId = "user-1", BoardId = "board-2" }
        };
        _repository.GetByUserIdAsync("user-1").Returns(forecasts);

        // Act
        var result = await _queries.GetForecastsByUserIdAsync("user-1");

        // Assert
        Assert.That(result.Succeeded, Is.True);
        Assert.That(result.Data!.Count(), Is.EqualTo(2));
        Assert.That(result.Data.All(f => f.UserId == "user-1"), Is.True);
    }
}
