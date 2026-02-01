using FantaTournament.Domain.Entities;
using FantaTournament.Domain.Services.Scoring.Rules;
using FantaTournament.Domain.ValueObjects;
using NUnit.Framework;

namespace FantaTournament.Domain.Tests.Services.Scoring.Rules;

[TestFixture]
public class RegularMatchRuleTests
{
    private RegularMatchRule _rule;

    [SetUp]
    public void SetUp()
    {
        _rule = new RegularMatchRule();
    }

    [Test]
    public void IsApplicable_ShouldReturnFalse_ForGroupStage()
    {
        Assert.That(_rule.IsApplicable(MatchPhase.GroupStage), Is.False);
    }

    [Test]
    public void IsApplicable_ShouldReturnTrue_ForKnockout()
    {
        Assert.That(_rule.IsApplicable(MatchPhase.RoundOf16), Is.True);
    }

    [Test]
    public void Calculate_CorrectGoalDifference_Returns1point5()
    {
        // Arrange
        // Match: 2-1 (Diff +1)
        var match = new Match 
        { 
            Code = "M01", 
            Phase = MatchPhase.RoundOf16, 
            Date = DateTime.Now, 
            HomeTeamPlaceholder = "H", 
            AwayTeamPlaceholder = "A",
            Result = new MatchResult(new Score(2, 1))
        };
        // Prediction: 1-0 (Diff +1)
        var prediction = new Prediction 
        { 
            MatchId = match.Id, 
            PredictedResult = new MatchResult(new Score(1, 0))
        };

        // Act
        var points = _rule.Calculate(match, prediction);

        // Assert
        Assert.That(points, Is.EqualTo(1.5));
    }

     [Test]
    public void Calculate_IncorrectGoalDifference_ReturnsZero()
    {
        // Arrange
        // Match: 2-1 (Diff +1)
        var match = new Match 
        { 
            Code = "M01", 
            Phase = MatchPhase.RoundOf16, 
            Date = DateTime.Now, 
            HomeTeamPlaceholder = "H", 
            AwayTeamPlaceholder = "A",
            Result = new MatchResult(new Score(2, 1))
        };
        // Prediction: 1-1 (Diff 0)
        var prediction = new Prediction 
        { 
            MatchId = match.Id, 
            PredictedResult = new MatchResult(new Score(1, 1))
        };

        // Act
        var points = _rule.Calculate(match, prediction);

        // Assert
        Assert.That(points, Is.EqualTo(0.0));
    }
}
