using FantaTournament.Domain.Entities;
using FantaTournament.Domain.Services.Scoring.Rules;
using FantaTournament.Domain.ValueObjects;
using NUnit.Framework;

namespace FantaTournament.Domain.Tests.Services.Scoring.Rules;

[TestFixture]
public class MatchWinnerRuleTests
{
    private MatchWinnerRule _rule;

    [SetUp]
    public void SetUp()
    {
        _rule = new MatchWinnerRule();
    }

    [Test]
    public void Calculate_GroupStage_CorrectWinner_Returns3Points()
    {
        // Arrange: Home Wins (2-1)
        var match = new Match 
        { 
            Code = "M01", 
            Phase = MatchPhase.GroupStage, 
            Date = DateTime.Now, 
            HomeTeamPlaceholder = "H", 
            AwayTeamPlaceholder = "A",
            Result = new MatchResult(new Score(2, 1))
        };
        // Predicted: Home Wins (1-0), different score but same winner
        var prediction = new Prediction 
        { 
            MatchId = match.Id, 
            PredictedResult = new MatchResult(new Score(1, 0))
        };

        // Act
        var points = _rule.Calculate(match, prediction);

        // Assert
        Assert.That(points, Is.EqualTo(3.0));
    }

    [Test]
    public void Calculate_Knockout_CorrectWinner_Returns5Points()
    {
        // Arrange: Away Wins (0-2)
        var match = new Match 
        { 
            Code = "M01", 
            Phase = MatchPhase.Final1_2, 
            Date = DateTime.Now, 
            HomeTeamPlaceholder = "H", 
            AwayTeamPlaceholder = "A",
            Result = new MatchResult(new Score(0, 2))
        };
        // Predicted: Away Wins (1-3)
        var prediction = new Prediction 
        { 
            MatchId = match.Id, 
            PredictedResult = new MatchResult(new Score(1, 3))
        };

        // Act
        var points = _rule.Calculate(match, prediction);

        // Assert
        Assert.That(points, Is.EqualTo(5.0));
    }

    [Test]
    public void Calculate_IncorrectWinner_ReturnsZero()
    {
        // Arrange: Draw (1-1)
        var match = new Match 
        { 
            Code = "M01", 
            Phase = MatchPhase.GroupStage, 
            Date = DateTime.Now, 
            HomeTeamPlaceholder = "H", 
            AwayTeamPlaceholder = "A",
            Result = new MatchResult(new Score(1, 1))
        };
        // Predicted: Home Wins (1-0)
        var prediction = new Prediction 
        { 
            MatchId = match.Id, 
            PredictedResult = new MatchResult(new Score(1, 0))
        };

        // Act
        var points = _rule.Calculate(match, prediction);

        // Assert
        Assert.That(points, Is.EqualTo(0.0));
    }

    [Test]
    public void Calculate_ExtraTimeConsidered()
    {
        // Arrange: Regular Time Draw (1-1), Extra Time Home Wins (2-1)
        var match = new Match 
        { 
            Code = "M01", 
            Phase = MatchPhase.Final1_2, 
            Date = DateTime.Now, 
            HomeTeamPlaceholder = "H", 
            AwayTeamPlaceholder = "A",
            Result = new MatchResult(new Score(1, 1), new Score(2, 1))
        };
        
        // Predicted: Home Wins in Regular Time (2-1) - This predicts Home Win
        // WAIT: Logic uses ExtraTime if available.
        // If Predicted has NO ExtraTime, it uses RegularTime.
        // Prediction: Home Wins (2-1) Regular Time.
        // Match: Home Wins (2-1) Extra Time.
        // Both imply Home Win.
        
        var prediction = new Prediction 
        { 
            MatchId = match.Id, 
            PredictedResult = new MatchResult(new Score(2, 1))
        };

        // The logic compares Sign(Actual) vs Sign(Predicted). 
        // Actual: (2-1) -> +1 (Home Win)
        // Predicted: (2-1) -> +1 (Home Win)
        
        // Act
        var points = _rule.Calculate(match, prediction);

        // Assert
        Assert.That(points, Is.EqualTo(5.0));
    }
}
