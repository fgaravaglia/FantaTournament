using FantaTournament.Infrastructure.Repositories;
using FantaTournament.Domain.Entities;
using FantaTournament.Domain.ValueObjects;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;
using System.Text.Json;

namespace FantaTournament.Application.Tests.Infrastructure;

[TestFixture]
public class JsonForecastRepositoryTests
{
    private string _testDataDirectory;
    private ILogger<JsonForecastRepository> _logger;

    [SetUp]
    public void SetUp()
    {
        var baseDir = AppDomain.CurrentDomain.BaseDirectory;
        _testDataDirectory = Path.Combine(baseDir, "test_data_" + Guid.NewGuid().ToString());
        
        if (!Directory.Exists(_testDataDirectory))
        {
            Directory.CreateDirectory(_testDataDirectory);
        }

        _logger = Substitute.For<ILogger<JsonForecastRepository>>();
    }

    [TearDown]
    public void TearDown()
    {
        if (Directory.Exists(_testDataDirectory))
        {
            Directory.Delete(_testDataDirectory, true);
        }
    }

    [Test]
    public async Task UpdateAsync_ShouldCreateJsonFile()
    {
        // Arrange
        var repository = new JsonForecastRepository(_testDataDirectory, _logger);
        var forecast = new Forecast
        {
            Id = "F1",
            UserId = "U1",
            BoardId = "B1",
            Predictions = new List<Prediction>
            {
                new Prediction { MatchId = "M1", PredictedResult = new MatchResult(new Score(2, 1), null) }
            }
        };

        // Act
        await repository.UpdateAsync(forecast);

        // Assert
        var expectedPath = Path.Combine(_testDataDirectory, "forecasts", "FORECAST-F1.json");
        Assert.That(File.Exists(expectedPath), Is.True);
        
        var json = await File.ReadAllTextAsync(expectedPath);
        var savedForecast = JsonSerializer.Deserialize<Forecast>(json);
        Assert.That(savedForecast, Is.Not.Null);
        Assert.That(savedForecast!.Id, Is.EqualTo("F1"));
        Assert.That(savedForecast.UserId, Is.EqualTo("U1"));
    }

    [Test]
    public async Task GetByIdAsync_ShouldReturnForecast()
    {
        // Arrange
        var repository = new JsonForecastRepository(_testDataDirectory, _logger);
        var forecast = new Forecast
        {
            Id = "F2",
            UserId = "U2",
            BoardId = "B2"
        };
        await repository.UpdateAsync(forecast);

        // Act
        var result = await repository.GetByIdAsync("F2");

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result!.Id, Is.EqualTo("F2"));
        Assert.That(result.UserId, Is.EqualTo("U2"));
    }

    [Test]
    public async Task GetByUserIdAsync_ShouldReturnAllUserForecasts()
    {
        // Arrange
        var repository = new JsonForecastRepository(_testDataDirectory, _logger);
        var f1 = new Forecast { Id = "F3", UserId = "U3", BoardId = "B1" };
        var f2 = new Forecast { Id = "F4", UserId = "U3", BoardId = "B2" };
        var f3 = new Forecast { Id = "F5", UserId = "U4", BoardId = "B1" };

        await repository.UpdateAsync(f1);
        await repository.UpdateAsync(f2);
        await repository.UpdateAsync(f3);

        // Act
        var results = (await repository.GetByUserIdAsync("U3")).ToList();

        // Assert
        Assert.That(results.Count, Is.EqualTo(2));
        Assert.That(results.Any(f => f.Id == "F3"), Is.True);
        Assert.That(results.Any(f => f.Id == "F4"), Is.True);
        Assert.That(results.Any(f => f.Id == "F5"), Is.False);
    }

    [Test]
    public async Task GetByIdAsync_WithNonExistentId_ShouldReturnNull()
    {
        // Arrange
        var repository = new JsonForecastRepository(_testDataDirectory, _logger);

        // Act
        var result = await repository.GetByIdAsync("NONEXISTENT");

        // Assert
        Assert.That(result, Is.Null);
    }
}
