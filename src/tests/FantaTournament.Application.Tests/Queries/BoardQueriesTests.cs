using FantaTournament.Application.Queries;
using FantaTournament.Domain.Entities;
using FantaTournament.Domain.Repositories;
using FantaTournament.Domain.ValueObjects;
using NSubstitute;
using NUnit.Framework;

namespace FantaTournament.Application.Tests.Queries;

[TestFixture]
public class BoardQueriesTests
{
    private IBoardRepository _repository;
    private BoardQueries _queries;

    [SetUp]
    public void SetUp()
    {
        _repository = Substitute.For<IBoardRepository>();
        _queries = new BoardQueries(_repository);
    }

    [Test]
    public async Task SearchBoardsAsync_ShouldReturnSuccess_WithDtos()
    {
        // Arrange
        var board = new Board { Name = "World Cup", Code = "WC2026" }; // Ensure required props are set
        _repository.SearchByNameAsync("World").Returns(new[] { board });

        // Act
        var result = await _queries.SearchBoardsAsync("World");

        // Assert
        Assert.That(result.Succeeded, Is.True);
        Assert.That(result.Data.Count(), Is.EqualTo(1));
        Assert.That(result.Data.First().Name, Is.EqualTo("World Cup"));
    }

    [Test]
    public async Task GetBoardMatchesAsync_WhenBoardExists_ShouldReturnMatches()
    {
        // Arrange
        var board = new Board { Name = "Euro 2028", Code = "EU28" };
        var match = new Match 
        { 
            Code = "M1", 
            Phase = MatchPhase.GroupStage, 
            Date = DateTime.Now, 
            HomeTeamPlaceholder = "H", 
            AwayTeamPlaceholder = "A" 
        };
        board.Matches.Add(match);
        
        _repository.GetByIdAsync("B1").Returns(board);

        // Act
        var result = await _queries.GetBoardMatchesAsync("B1");

        // Assert
        Assert.That(result.Succeeded, Is.True);
        Assert.That(result.Data.Matches.Count, Is.EqualTo(1));
        Assert.That(result.Data.BoardName, Is.EqualTo("Euro 2028"));
    }

    [Test]
    public async Task GetBoardMatchesAsync_WhenBoardNotFound_ShouldReturnNotFound()
    {
        // Arrange
        _repository.GetByIdAsync("B1").Returns((Board?)null);

        // Act
        var result = await _queries.GetBoardMatchesAsync("B1");

        // Assert
        Assert.That(result.Succeeded, Is.False);
        Assert.That(result.Errors, Contains.Item("Not Found"));
    }

    [Test]
    public async Task GetBoardTeamsAsync_ShouldReturnDistinctTeams()
    {
        // Arrange
        var team1 = new Team { Name = "Italy", BoardCode = "ITA" };
        var team2 = new Team { Name = "France", BoardCode = "FRA" };
        var board = new Board { Name = "Board", Code = "B" };
        
        var match1 = new Match 
        { 
            Code = "M1", Phase = MatchPhase.GroupStage, Date = DateTime.Now, 
            HomeTeam = team1, AwayTeam = team2,
            HomeTeamPlaceholder = "H1", AwayTeamPlaceholder = "A1"
        };
        // Reuse same teams
        var match2 = new Match 
        { 
            Code = "M2", Phase = MatchPhase.GroupStage, Date = DateTime.Now, 
            HomeTeam = team2, AwayTeam = team1,
            HomeTeamPlaceholder = "H2", AwayTeamPlaceholder = "A2"
        };
        
        board.Matches.Add(match1);
        board.Matches.Add(match2);

        _repository.GetByIdAsync("B1").Returns(board);

        // Act
        var result = await _queries.GetBoardTeamsAsync("B1");

        // Assert
        Assert.That(result.Succeeded, Is.True);
        Assert.That(result.Data.Count(), Is.EqualTo(2)); // Should be 2 distinct teams, not 4 references
        Assert.That(result.Data.Select(t => t.Name), Contains.Item("Italy"));
        Assert.That(result.Data.Select(t => t.Name), Contains.Item("France"));
    }
}
