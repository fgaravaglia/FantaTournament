using FantaTournament.Domain.Entities;
using FantaTournament.Domain.Services;
using FantaTournament.Domain.ValueObjects;
using NSubstitute;
using NUnit.Framework;

namespace FantaTournament.Domain.Tests.Entities;

[TestFixture]
public class ForecastTests
{
    [Test]
    public void RecalculateScore_ShouldUpdateTotalScoreAndPredictionScores()
    {
        // Arrange
        var policy = Substitute.For<IScoringPolicy>();
        var board = new Board { Name = "Test Board" };
        var match1 = new Match 
        { 
            Code = "M1", 
            Phase = MatchPhase.GroupStage, 
            Date = DateTime.Now, 
            Status = MatchStatus.Played, 
            Result = new MatchResult(new Score(1, 0)),
            HomeTeamPlaceholder="H", AwayTeamPlaceholder="A" 
        };
        var match2 = new Match 
        { 
            Code = "M2", 
            Phase = MatchPhase.GroupStage, 
            Date = DateTime.Now, 
            Status = MatchStatus.Scheduled, // Not Played
            Result = null,
            HomeTeamPlaceholder="H", AwayTeamPlaceholder="A" 
        };
        
        board.Matches.Add(match1);
        board.Matches.Add(match2);

        var prediction1 = new Prediction 
        { 
            MatchId = match1.Id, 
            PredictedResult = new MatchResult(new Score(1, 0)) 
        };
        var prediction2 = new Prediction 
        { 
            MatchId = match2.Id, 
            PredictedResult = new MatchResult(new Score(0, 0)) 
        };

        var forecast = new Forecast 
        { 
            UserId = "User1", 
            BoardId = board.Id, 
            Predictions = new List<Prediction> { prediction1, prediction2 } 
        };

        policy.CalculateScore(match1, prediction1).Returns(10.0);
        policy.CalculateScore(match2, prediction2).Returns(50.0); // Should not be called/used

        // Act
        forecast.RecalculateScore(policy, board);

        // Assert
        Assert.That(forecast.TotalScore, Is.EqualTo(10.0));
        Assert.That(prediction1.Score, Is.EqualTo(10.0));
        Assert.That(prediction2.Score, Is.EqualTo(0.0)); // Match2 not played
    }

    [Test]
    public void RecalculateScore_WithUnknownMatchId_ShouldIgnorePrediction()
    {
        // Arrange
        var policy = Substitute.For<IScoringPolicy>();
        var board = new Board { Name = "Test Board" }; // Empty board
        var forecast = new Forecast 
        { 
            UserId = "U1", 
            BoardId = board.Id, 
            Predictions = new List<Prediction> 
            { 
                new Prediction { MatchId = "Unknown", PredictedResult = new MatchResult(new Score(1, 1)) } 
            }
        };

        // Act
        forecast.RecalculateScore(policy, board);

        // Assert
        Assert.That(forecast.TotalScore, Is.EqualTo(0.0));
    }
}
