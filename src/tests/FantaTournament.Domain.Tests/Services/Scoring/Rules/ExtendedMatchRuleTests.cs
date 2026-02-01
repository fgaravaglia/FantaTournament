using FantaTournament.Domain.Entities;
using FantaTournament.Domain.Services.Scoring.Rules;
using FantaTournament.Domain.ValueObjects;
using NUnit.Framework;

namespace FantaTournament.Domain.Tests.Services.Scoring.Rules;

[TestFixture]
public class ExtendedMatchRuleTests
{
    private ExtendedMatchRule _rule;

    [SetUp]
    public void SetUp()
    {
        _rule = new ExtendedMatchRule();
    }

    [Test]
    public void Calculate_CorrectExtraTimeDiff_Returns1point5()
    {
        // Arrange
        var match = new Match 
        { 
            Code = "M01", 
            Phase = MatchPhase.RoundOf16, 
            Date = DateTime.Now, 
            HomeTeamPlaceholder = "H", 
            AwayTeamPlaceholder = "A",
            Result = new MatchResult(new Score(1, 1), new Score(3, 2)) // ET: 3-2 (Diff +1)
        };
        
        var prediction = new Prediction 
        { 
            MatchId = match.Id, 
            PredictedResult = new MatchResult(new Score(1, 1), new Score(2, 1)) // ET: 2-1 (Diff +1)
        };

        // Act
        var points = _rule.Calculate(match, prediction);

        // Assert
        Assert.That(points, Is.EqualTo(1.5));
    }
    
    [Test]
    public void Calculate_NoExtraTimeInMatch_ReturnsZero()
    {
        // Arrange
        var match = new Match 
        { 
            Code = "M01", 
            Phase = MatchPhase.RoundOf16, 
            Date = DateTime.Now, 
            HomeTeamPlaceholder = "H", 
            AwayTeamPlaceholder = "A",
            Result = new MatchResult(new Score(1, 1), null) // No ET
        };
        
        var prediction = new Prediction 
        { 
            MatchId = match.Id, 
            PredictedResult = new MatchResult(new Score(1, 1), new Score(2, 1))
        };

        // Act
        var points = _rule.Calculate(match, prediction);

        // Assert
        Assert.That(points, Is.EqualTo(0.0));
    }
}
