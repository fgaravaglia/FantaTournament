using FantaTournament.Domain.Entities;
using FantaTournament.Domain.Services.Scoring.Rules;
using FantaTournament.Domain.ValueObjects;
using NUnit.Framework;

namespace FantaTournament.Domain.Tests.Services.Scoring.Rules;

[TestFixture]
public class ExactMatchRuleTests
{
    private ExactMatchRule _rule;

    [SetUp]
    public void SetUp()
    {
        _rule = new ExactMatchRule();
    }

    [Test]
    public void IsApplicable_ShouldReturnTrue_ForAnyPhase()
    {
        Assert.That(_rule.IsApplicable(MatchPhase.GroupStage), Is.True);
        Assert.That(_rule.IsApplicable(MatchPhase.Final1_2), Is.True);
    }

    // [TestCase] removed due to non-constant arguments with Smart Enums. 
    // Logic is covered by specific tests below.
    
    [Test]
    public void Calculate_GroupStage_ExactMatch_Returns3Points()
    {
        // Arrange
        var score = new Score(2, 1);
        var result = new MatchResult(score);
        var match = new Match 
        { 
            Code = "M01", 
            Phase = MatchPhase.GroupStage, 
            Date = DateTime.Now, 
            HomeTeamPlaceholder = "H", 
            AwayTeamPlaceholder = "A",
            Result = result
        };
        var prediction = new Prediction 
        { 
            MatchId = match.Id, 
            PredictedResult = result 
        };

        // Act
        var points = _rule.Calculate(match, prediction);

        // Assert
        Assert.That(points, Is.EqualTo(3.0));
    }

    [Test]
    public void Calculate_Knockout_ExactMatch_Returns5Points()
    {
        // Arrange
        var score = new Score(2, 1);
        var result = new MatchResult(score);
        var match = new Match 
        { 
            Code = "M01", 
            Phase = MatchPhase.RoundOf16, 
            Date = DateTime.Now, 
            HomeTeamPlaceholder = "H", 
            AwayTeamPlaceholder = "A",
            Result = result
        };
        var prediction = new Prediction 
        { 
            MatchId = match.Id, 
            PredictedResult = result 
        };

        // Act
        var points = _rule.Calculate(match, prediction);

        // Assert
        Assert.That(points, Is.EqualTo(5.0));
    }

    [Test]
    public void Calculate_NotExactMatch_ReturnsZero()
    {
        // Arrange
        var match = new Match 
        { 
            Code = "M01", 
            Phase = MatchPhase.GroupStage, 
            Date = DateTime.Now, 
            HomeTeamPlaceholder = "H", 
            AwayTeamPlaceholder = "A",
            Result = new MatchResult(new Score(2, 1))
        };
        var prediction = new Prediction 
        { 
            MatchId = match.Id, 
            PredictedResult = new MatchResult(new Score(1, 0)) // Different score
        };

        // Act
        var points = _rule.Calculate(match, prediction);

        // Assert
        Assert.That(points, Is.EqualTo(0.0));
    }
    
    [Test]
    public void Calculate_ResultNull_ReturnsZero()
    {
        // Arrange
        var match = new Match 
        { 
            Code = "M01", 
            Phase = MatchPhase.GroupStage, 
            Date = DateTime.Now, 
            HomeTeamPlaceholder = "H", 
            AwayTeamPlaceholder = "A",
            Result = null
        };
        var prediction = new Prediction { MatchId = match.Id, PredictedResult = new MatchResult(new Score(1, 0)) };

        // Act
        var points = _rule.Calculate(match, prediction);

        // Assert
        Assert.That(points, Is.EqualTo(0.0));
    }
}
