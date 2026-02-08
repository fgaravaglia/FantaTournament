using FantaTournament.Application.Commands;
using FantaTournament.Application.DTOs;
using FantaTournament.Domain.Entities;
using FantaTournament.Domain.Repositories;
using NSubstitute;
using NUnit.Framework;
using Umbrella.Core;

namespace FantaTournament.Application.Tests.Commands;

[TestFixture]
public class ForecastCommandsTests
{
    private IForecastRepository _repository;
    private ForecastCommands _commands;

    [SetUp]
    public void SetUp()
    {
        _repository = Substitute.For<IForecastRepository>();
        _commands = new ForecastCommands(_repository);
    }

    [Test]
    public async Task CreateForecastAsync_ShouldCallUpdateAsyncAndReturnSuccess()
    {
        // Arrange
        var dto = new ForecastDto
        {
            Id = "F1",
            UserId = "U1",
            BoardId = "B1",
            Predictions = new List<PredictionDto>
            {
                new PredictionDto { MatchId = "M1", PredictedHomeScore = 2, PredictedAwayScore = 1 }
            }
        };

        // Act
        var result = await _commands.CreateForecastAsync(dto);

        // Assert
        Assert.That(result.Succeeded, Is.True);
        Assert.That(result.Data, Is.EqualTo("F1"));
        await _repository.Received(1).UpdateAsync(Arg.Is<Forecast>(f => f.Id == "F1" && f.UserId == "U1"));
    }

    [Test]
    public async Task UpdateForecastAsync_WithExistingForecast_ShouldCallUpdateAsyncAndReturnSuccess()
    {
        // Arrange
        var existingForecast = new Forecast { Id = "F1", UserId = "U1", BoardId = "B1" };
        _repository.GetByIdAsync("F1").Returns(existingForecast);

        var dto = new ForecastDto
        {
            Id = "F1",
            UserId = "U1",
            BoardId = "B1",
            Predictions = new List<PredictionDto>
            {
                new PredictionDto { MatchId = "M1", PredictedHomeScore = 3, PredictedAwayScore = 2 }
            }
        };

        // Act
        var result = await _commands.UpdateForecastAsync(dto);

        // Assert
        Assert.That(result.Succeeded, Is.True);
        Assert.That(result.Data, Is.EqualTo("F1"));
        await _repository.Received(1).UpdateAsync(Arg.Is<Forecast>(f => f.Id == "F1" && f.Predictions.Count == 1));
    }

    [Test]
    public async Task UpdateForecastAsync_WithNonExistentForecast_ShouldReturnNotFound()
    {
        // Arrange
        _repository.GetByIdAsync("NONEXISTENT").Returns((Forecast)null);
        var dto = new ForecastDto { Id = "NONEXISTENT", UserId = "U1", BoardId = "B1" };

        // Act
        var result = await _commands.UpdateForecastAsync(dto);

        // Assert
        Assert.That(result.Succeeded, Is.False);
        Assert.That(result.Errors, Contains.Item("Not Found"));
    }

    [Test]
    public async Task DeleteForecastAsync_WithExistingForecast_ShouldCallDeleteAsyncAndReturnSuccess()
    {
        // Arrange
        var existingForecast = new Forecast { Id = "F1", UserId = "U1", BoardId = "B1" };
        _repository.GetByIdAsync("F1").Returns(existingForecast);

        // Act
        var result = await _commands.DeleteForecastAsync("F1");

        // Assert
        Assert.That(result.Succeeded, Is.True);
        await _repository.Received(1).DeleteAsync("F1");
    }

    [Test]
    public async Task DeleteForecastAsync_WithNonExistentForecast_ShouldReturnNotFound()
    {
        // Arrange
        _repository.GetByIdAsync("NONEXISTENT").Returns((Forecast)null);

        // Act
        var result = await _commands.DeleteForecastAsync("NONEXISTENT");

        // Assert
        Assert.That(result.Succeeded, Is.False);
        Assert.That(result.Errors, Contains.Item("Not Found"));
    }
}
