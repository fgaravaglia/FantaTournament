using FantaTournament.Domain.ValueObjects;
using NUnit.Framework;

namespace FantaTournament.Domain.Tests.ValueObjects;

[TestFixture]
public class MatchStatusTests
{
    [Test]
    public void Scheduled_ShouldHaveCorrectCodeAndDisplayName()
    {
        // Arrange & Act
        var status = MatchStatus.Scheduled;

        // Assert
        Assert.That(status.Code, Is.EqualTo("SCHEDULED"));
        Assert.That(status.DisplayName, Is.EqualTo("Scheduled"));
    }

    [Test]
    public void InProgress_ShouldHaveCorrectCodeAndDisplayName()
    {
        // Arrange & Act
        var status = MatchStatus.InProgress;

        // Assert
        Assert.That(status.Code, Is.EqualTo("IN_PROGRESS"));
        Assert.That(status.DisplayName, Is.EqualTo("In Progress"));
    }

    [Test]
    public void Played_ShouldHaveCorrectCodeAndDisplayName()
    {
        // Arrange & Act
        var status = MatchStatus.Played;

        // Assert
        Assert.That(status.Code, Is.EqualTo("PLAYED"));
        Assert.That(status.DisplayName, Is.EqualTo("Played"));
    }

    [Test]
    public void IsFinished_ShouldReturnTrue_WhenStatusIsPlayed()
    {
        // Arrange
        var status = MatchStatus.Played;

        // Act
        var result = status.IsFinished();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsFinished_ShouldReturnFalse_WhenStatusIsScheduled()
    {
        // Arrange
        var status = MatchStatus.Scheduled;

        // Act
        var result = status.IsFinished();

        // Assert
        Assert.That(result, Is.False);
    }
}
