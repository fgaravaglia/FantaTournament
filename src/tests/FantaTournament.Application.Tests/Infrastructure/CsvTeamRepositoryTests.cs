using FantaTournament.Infrastructure.Repositories;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;

namespace FantaTournament.Application.Tests.Infrastructure;

[TestFixture]
public class CsvTeamRepositoryTests
{
    private string _dataDirectory;
    private ILogger<CsvTeamRepository> _logger;

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

        _logger = Substitute.For<ILogger<CsvTeamRepository>>();
    }

    private async Task CreateTestFile(string boardId, string content)
    {
        var filePath = Path.Combine(_dataDirectory, $"TEAMS-{boardId}.csv");
        await File.WriteAllTextAsync(filePath, content);
    }

    [Test]
    public async Task GetByBoardIdAsync_WithExistingFile_ShouldReturnTeams()
    {
        // Arrange
        var boardId = "TEST_BOARD";
        var content = "Team;DisplayName;BoardCode\nGER;Germany;A1\nSCO;Scotland;A2";
        await CreateTestFile(boardId, content);

        var repository = new CsvTeamRepository(_dataDirectory, _logger);

        // Act
        var teams = await repository.GetByBoardIdAsync(boardId);

        // Assert
        Assert.That(teams, Is.Not.Null);
        var teamList = teams.ToList();
        Assert.That(teamList.Count, Is.EqualTo(2));
        
        var germany = teamList.FirstOrDefault(t => t.Id == "GER");
        Assert.That(germany, Is.Not.Null);
        Assert.That(germany!.Name, Is.EqualTo("Germany"));
        Assert.That(germany.BoardCode, Is.EqualTo("A1"));
    }

    [Test]
    public async Task GetByBoardIdAsync_WithMissingFile_ShouldReturnEmptyCollection()
    {
        // Arrange
        var repository = new CsvTeamRepository(_dataDirectory, _logger);
        var boardId = "NONEXISTENT";

        // Act
        var teams = await repository.GetByBoardIdAsync(boardId);

        // Assert
        Assert.That(teams, Is.Empty);
    }
}
