using FantaTournament.Domain.Entities;
using FantaTournament.Domain.Services.Scoring;
using FantaTournament.Domain.Services.Scoring.Rules;
using FantaTournament.Domain.ValueObjects;
using NSubstitute;
using NUnit.Framework;

namespace FantaTournament.Domain.Tests.Services.Scoring;

[TestFixture]
public class ScoringServiceTests
{
    private ScoringService _service;
    private IScoringRule _mockRule1;
    private IScoringRule _mockRule2;
    private Match _match;
    private Prediction _prediction;

    [SetUp]
    public void SetUp()
    {
        _mockRule1 = Substitute.For<IScoringRule>();
        _mockRule2 = Substitute.For<IScoringRule>();
        _service = new ScoringService(new[] { _mockRule1, _mockRule2 });
        _match = new Match 
        { 
            Code = "M01", 
            Phase = MatchPhase.GroupStage, 
            Date = DateTime.Now, 
            HomeTeamPlaceholder = "H", 
            AwayTeamPlaceholder = "A",
            Result = new MatchResult(new Score(1, 0)) 
        };
        _prediction = new Prediction 
        { 
            MatchId = _match.Id, 
            PredictedResult = new MatchResult(new Score(1, 0)) 
        };
    }

    [Test]
    public void CalculateScore_ShouldSumPointsFromAllApplicableRules()
    {
        // Arrange
        _mockRule1.IsApplicable(MatchPhase.GroupStage).Returns(true);
        _mockRule1.Calculate(_match, _prediction).Returns(3.0);

        _mockRule2.IsApplicable(MatchPhase.GroupStage).Returns(true);
        _mockRule2.Calculate(_match, _prediction).Returns(5.0);

        // Act
        var total = _service.CalculateScore(_match, _prediction);

        // Assert
        Assert.That(total, Is.EqualTo(8.0));
    }

    [Test]
    public void CalculateScore_ShouldIgnoreNonApplicableRules()
    {
        // Arrange
        _mockRule1.IsApplicable(MatchPhase.GroupStage).Returns(true);
        _mockRule1.Calculate(_match, _prediction).Returns(3.0);

        _mockRule2.IsApplicable(MatchPhase.GroupStage).Returns(false);
        _mockRule2.Calculate(_match, _prediction).Returns(100.0); // Should be ignored

        // Act
        var total = _service.CalculateScore(_match, _prediction);

        // Assert
        Assert.That(total, Is.EqualTo(3.0));
    }
    
    [Test]
    public void CalculateScore_WhenNoRulesApplicable_ReturnsZero()
    {
        // Arrange
        _mockRule1.IsApplicable(MatchPhase.GroupStage).Returns(false);
        _mockRule2.IsApplicable(MatchPhase.GroupStage).Returns(false);

        // Act
        var total = _service.CalculateScore(_match, _prediction);

        // Assert
        Assert.That(total, Is.EqualTo(0.0));
    }
}
