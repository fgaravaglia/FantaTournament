using FantaTournament.Application.Commands;
using FantaTournament.Application.DTOs;
using FantaTournament.Domain.Entities;
using FantaTournament.Domain.Repositories;
using FantaTournament.Domain.ValueObjects;
using NSubstitute;
using NUnit.Framework;

namespace FantaTournament.Application.Tests.Commands;

[TestFixture]
public class BoardCommandsTests
{
    private IBoardRepository _repository;
    private BoardCommands _commands;

    [SetUp]
    public void SetUp()
    {
        _repository = Substitute.For<IBoardRepository>();
        _commands = new BoardCommands(_repository);
    }

    [Test]
    public async Task UpdateMatchResultAsync_ShouldUpdateAndSave_WhenMatchExists()
    {
        // Arrange
        var board = new Board { Name = "B1", Code = "C1" };
        var match = new Match 
        { 
            Code = "M1", Phase = MatchPhase.GroupStage, Date = DateTime.Now, 
            HomeTeamPlaceholder = "H", AwayTeamPlaceholder = "A",
            Status = MatchStatus.Scheduled
        };
        board.Matches.Add(match); // ID will be auto-generated or Guid
        var matchId = match.Id;

        _repository.GetByIdAsync(board.Id).Returns(board);

        var newResult = new MatchResult(new Score(2, 1));

        // Act
        var result = await _commands.UpdateMatchResultAsync(board.Id, matchId, newResult);

        // Assert
        Assert.That(result.Succeeded, Is.True);
        Assert.That(match.Result, Is.EqualTo(newResult));
        await _repository.Received(1).UpdateAsync(board);
    }

    [Test]
    public async Task UpdateMatchStatusAsync_ShouldUpdateAndSave()
    {
        // Arrange
        var board = new Board { Name = "B1", Code = "C1" };
        var match = new Match 
        { 
            Code = "M1", Phase = MatchPhase.GroupStage, Date = DateTime.Now, 
            HomeTeamPlaceholder = "H", AwayTeamPlaceholder = "A",
            Status = MatchStatus.Scheduled
        };
        board.Matches.Add(match);
        var matchId = match.Id;

        _repository.GetByIdAsync(board.Id).Returns(board);

        // Act
        var result = await _commands.UpdateMatchStatusAsync(board.Id, matchId, MatchStatus.InProgress);

        // Assert
        Assert.That(result.Succeeded, Is.True);
        Assert.That(match.Status, Is.EqualTo(MatchStatus.InProgress));
        await _repository.Received(1).UpdateAsync(board);
    }

    [Test]
    public async Task ImportMatchesAsync_ShouldAddMatchesAndSave()
    {
        // Arrange
        var board = new Board { Name = "B1", Code = "C1" };
        _repository.GetByIdAsync(board.Id).Returns(board);

        var dtos = new List<MatchDto>
        {
            new MatchDto 
            { 
                Code = "M1", 
                Phase = "GroupStage", 
                Date = DateTime.Now.AddDays(1), 
                HomeTeam = "Italy", 
                AwayTeam = "France" 
            },
            new MatchDto 
            { 
                Code = "M2", 
                Phase = "Final1_2", 
                Date = DateTime.Now.AddDays(10), 
                HomeTeam = "Winner A", 
                AwayTeam = "Winner B" 
            }
        };

        // Act
        var result = await _commands.ImportMatchesAsync(board.Id, dtos);

        // Assert
        Assert.That(result.Succeeded, Is.True);
        Assert.That(board.Matches.Count, Is.EqualTo(2));
        
        var m1 = board.Matches.First(m => m.Code == "M1");
        Assert.That(m1.Phase, Is.EqualTo(MatchPhase.GroupStage));
        Assert.That(m1.HomeTeamPlaceholder, Is.EqualTo("Italy"));
        
        var m2 = board.Matches.First(m => m.Code == "M2");
        Assert.That(m2.Phase, Is.EqualTo(MatchPhase.Final1_2));
        
        await _repository.Received(1).UpdateAsync(board);
    }
}
