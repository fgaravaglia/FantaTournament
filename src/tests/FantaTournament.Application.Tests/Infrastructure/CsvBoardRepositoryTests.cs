using FantaTournament.Infrastructure.Repositories;
using FantaTournament.Domain.Repositories;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;
using FantaTournament.Domain.Entities;

namespace FantaTournament.Application.Tests.Infrastructure;

[TestFixture]
public class CsvBoardRepositoryTests
{
    private string _dataDirectory;
    private ITeamRepository _teamRepository;
    private ILogger<CsvBoardRepository> _logger;

    [SetUp]
    public void SetUp()
    {
        // Option 1: Create a data folder in the test project root
        var baseDir = AppDomain.CurrentDomain.BaseDirectory;
        _dataDirectory = Path.Combine(baseDir, "..", "..", "..", "data");

        if (!Directory.Exists(_dataDirectory))
        {
            Directory.CreateDirectory(_dataDirectory);
        }

        _teamRepository = Substitute.For<ITeamRepository>();
        _logger = Substitute.For<ILogger<CsvBoardRepository>>();
    }

    private async Task CreateMatchFile(string boardId, string content)
    {
        var filePath = Path.Combine(_dataDirectory, $"BoardMatches_{boardId}.csv");
        await File.WriteAllTextAsync(filePath, content);
    }

    private async Task CreateBoardsFile(string content)
    {
        var filePath = Path.Combine(_dataDirectory, "BOARDS.csv");
        await File.WriteAllTextAsync(filePath, content);
    }

    [Test]
    public async Task GetByIdAsync_WithExistingFiles_ShouldReturnHydratedBoard()
    {
        // Arrange
        var boardId = "TEST_BOARD";
        var boardName = "Test Tournament";
        await CreateBoardsFile($"ID;DisplayName\n{boardId};{boardName}");
        
        var matchContent = "Key;Container;TeamA;TeamB;MatchType;Date;NGoalA;NGoalB;NFinalA;NFinalB;IsStarted\nA-GER-SCO;A;GER;SCO;ROUND;14/06/2024 21.00;0;0;0;0;0";
        await CreateMatchFile(boardId, matchContent);

        var mockTeams = new List<Team>
        {
            new Team { Id = "GER", Name = "Germany", BoardCode = "A1" },
            new Team { Id = "SCO", Name = "Scotland", BoardCode = "A2" }
        };
        _teamRepository.GetByBoardIdAsync(boardId).Returns(mockTeams);

        var repository = new CsvBoardRepository(_dataDirectory, _teamRepository, _logger);

        // Act
        var board = await repository.GetByIdAsync(boardId);

        // Assert
        Assert.That(board, Is.Not.Null);
        Assert.That(board!.Id, Is.EqualTo(boardId));
        Assert.That(board.Name, Is.EqualTo(boardName));
        Assert.That(board.Teams.Count, Is.EqualTo(2));
        Assert.That(board.Matches.Count, Is.EqualTo(1));

        // Check first match (GER vs SCO)
        var firstMatch = board.Matches.FirstOrDefault(m => m.Code == "A-GER-SCO");
        Assert.That(firstMatch, Is.Not.Null);
        Assert.That(firstMatch!.HomeTeam, Is.Not.Null);
        Assert.That(firstMatch.HomeTeam!.Id, Is.EqualTo("GER"));
        Assert.That(firstMatch.AwayTeam, Is.Not.Null);
        Assert.That(firstMatch.AwayTeam!.Id, Is.EqualTo("SCO"));
        Assert.That(firstMatch.Date.Year, Is.EqualTo(2024));
    }

    [Test]
    public async Task GetByIdAsync_WithMissingFile_ShouldReturnNull()
    {
        // Arrange
        var repository = new CsvBoardRepository(_dataDirectory, _teamRepository, _logger);

        // Act
        var board = await repository.GetByIdAsync("NONEXISTENT");

        // Assert
        Assert.That(board, Is.Null);
    }
    [Test]
    public async Task SearchByNameAsync_WithMatches_ShouldReturnBoards()
    {
        // Arrange
        await CreateBoardsFile("ID;DisplayName\nEURO2024;UEFA Euro 2024\nWC2026;FIFA World Cup 2026");
        var repository = new CsvBoardRepository(_dataDirectory, _teamRepository, _logger);

        // Act
        var results = await repository.SearchByNameAsync("World");

        // Assert
        Assert.That(results, Is.Not.Null);
        var list = results.ToList();
        Assert.That(list.Count, Is.EqualTo(1));
        Assert.That(list[0].Id, Is.EqualTo("WC2026"));
        Assert.That(list[0].Name, Is.EqualTo("FIFA World Cup 2026"));
    }
}
