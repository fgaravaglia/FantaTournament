using FantaTournament.Domain.Entities;
using FantaTournament.Domain.Services;
using NUnit.Framework;

namespace FantaTournament.Domain.Tests.Services;

[TestFixture]
public class RankingServiceTests
{
    private RankingService _service;

    [SetUp]
    public void SetUp()
    {
        _service = new RankingService();
    }

    [Test]
    public void CalculateGlobalRanking_ShouldSortByScoreDescending()
    {
        // Arrange
        var f1 = new Forecast { UserId = "U1", BoardId = "B1", TotalScore = 10, LastUpdatedAt = DateTime.Now };
        var f2 = new Forecast { UserId = "U2", BoardId = "B1", TotalScore = 20, LastUpdatedAt = DateTime.Now };
        var f3 = new Forecast { UserId = "U3", BoardId = "B1", TotalScore = 5, LastUpdatedAt = DateTime.Now };
        var forecasts = new[] { f1, f2, f3 };

        // Act
        var ranking = _service.CalculateGlobalRanking("B1", forecasts);

        // Assert
        Assert.That(ranking.Entries[0].UserId, Is.EqualTo("U2")); // 20
        Assert.That(ranking.Entries[1].UserId, Is.EqualTo("U1")); // 10
        Assert.That(ranking.Entries[2].UserId, Is.EqualTo("U3")); // 5
        Assert.That(ranking.Entries[0].Position, Is.EqualTo(1));
    }

    [Test]
    public void CalculateGlobalRanking_ShouldApplyTieBreaker_OldestIsFirst()
    {
        // Arrange
        var time = DateTime.Now;
        var f1 = new Forecast { UserId = "LateUser", BoardId = "B1", TotalScore = 10, LastUpdatedAt = time.AddMinutes(10) };
        var f2 = new Forecast { UserId = "EarlyUser", BoardId = "B1", TotalScore = 10, LastUpdatedAt = time }; // Older
        var forecasts = new[] { f1, f2 };

        // Act
        var ranking = _service.CalculateGlobalRanking("B1", forecasts);

        // Assert
        Assert.That(ranking.Entries[0].UserId, Is.EqualTo("EarlyUser")); // Same score, but updated earlier
        Assert.That(ranking.Entries[1].UserId, Is.EqualTo("LateUser"));
    }

    [Test]
    public void CalculateLeagueRanking_ShouldFilterMembers()
    {
        // Arrange
        var league = new League { Id = "L1", BoardId = "B1" };
        league.AddMember("Member1");
        
        var f1 = new Forecast { UserId = "Member1", BoardId = "B1", TotalScore = 10 };
        var f2 = new Forecast { UserId = "NonMember", BoardId = "B1", TotalScore = 20 };
        var forecasts = new[] { f1, f2 };

        // Act
        var ranking = _service.CalculateLeagueRanking(league, forecasts);

        // Assert
        Assert.That(ranking.Entries.Count, Is.EqualTo(1));
        Assert.That(ranking.Entries[0].UserId, Is.EqualTo("Member1"));
        Assert.That(ranking.LeagueId, Is.EqualTo("L1"));
    }
}
