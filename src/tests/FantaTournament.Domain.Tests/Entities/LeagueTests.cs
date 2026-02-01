using FantaTournament.Domain.Entities;
using NUnit.Framework;

namespace FantaTournament.Domain.Tests.Entities;

[TestFixture]
public class LeagueTests
{
    [Test]
    public void AddMember_ShouldAddUser_WhenNotAlreadyMember()
    {
        // Arrange
        var league = new League();
        var userId = "User1";

        // Act
        league.AddMember(userId);

        // Assert
        Assert.That(league.MemberIds, Contains.Item(userId));
        Assert.That(league.MemberIds.Count, Is.EqualTo(1));
    }

    [Test]
    public void AddMember_ShouldNotDuplicate_WhenAlreadyMember()
    {
        // Arrange
        var league = new League();
        var userId = "User1";
        league.AddMember(userId);

        // Act
        league.AddMember(userId);

        // Assert
        Assert.That(league.MemberIds.Count, Is.EqualTo(1));
    }

    [Test]
    public void RemoveMember_ShouldRemoveUser_WhenMember()
    {
        // Arrange
        var league = new League();
        var userId = "User1";
        league.AddMember(userId);

        // Act
        league.RemoveMember(userId);

        // Assert
        Assert.That(league.MemberIds, Does.Not.Contain(userId));
    }
}
