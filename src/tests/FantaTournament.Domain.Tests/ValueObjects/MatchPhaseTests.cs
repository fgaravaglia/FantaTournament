using FantaTournament.Domain.ValueObjects;
using NUnit.Framework;

namespace FantaTournament.Domain.Tests.ValueObjects;

[TestFixture]
public class MatchPhaseTests
{
    [Test]
    public void GroupStage_ShouldHaveCorrectNameAndOrder()
    {
        // Arrange & Act
        var phase = MatchPhase.GroupStage;

        // Assert
        Assert.That(phase.Name, Is.EqualTo("GroupStage"));
        Assert.That(phase.Order, Is.EqualTo(1));
    }
    
    [Test]
    public void Final1_2_ShouldHaveCorrectNameAndOrder()
    {
        // Arrange & Act
        var phase = MatchPhase.Final1_2;

        // Assert
        Assert.That(phase.Name, Is.EqualTo("Final1_2"));
        Assert.That(phase.Order, Is.EqualTo(6));
    }

    [Test]
    public void Equality_ShouldWork()
    {
         // Arrange
         var phase1 = MatchPhase.GroupStage;
         var phase2 = MatchPhase.GroupStage;
         var phase3 = MatchPhase.RoundOf16;

         // Act & Assert
         Assert.That(phase1, Is.EqualTo(phase2));
         Assert.That(phase1, Is.Not.EqualTo(phase3));
    }
}
