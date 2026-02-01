using FantaTournament.Domain.Entities;
using FantaTournament.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace FantaTournament.Infrastructure.Repositories;

/// <summary>
/// Implementation of <see cref="ITeamRepository"/> that reads team data from CSV files.
/// </summary>
internal class CsvTeamRepository : ITeamRepository
{
    private readonly string _dataDirectory;
    private readonly ILogger<CsvTeamRepository> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="CsvTeamRepository"/> class.
    /// </summary>
    /// <param name="dataDirectory">The directory where CSV files are stored.</param>
    /// <param name="logger">The logger instance.</param>
    public CsvTeamRepository(string dataDirectory, ILogger<CsvTeamRepository> logger)
    {
        _dataDirectory = dataDirectory;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Team>> GetByBoardIdAsync(string boardId)
    {
        var filePath = Path.Combine(_dataDirectory, $"TEAMS-{boardId}.csv");

        if (!File.Exists(filePath))
        {
            _logger.LogWarning("CSV file not found: {FilePath}", filePath);
            return Enumerable.Empty<Team>();
        }

        var teams = new List<Team>();

        try
        {
            var lines = await File.ReadAllLinesAsync(filePath);

            // Skip header: Team;DisplayName;BoardCode
            foreach (var line in lines.Skip(1))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                var parts = line.Split(';');
                if (parts.Length < 3)
                {
                    _logger.LogWarning("Invalid line format in {FilePath}: {Line}", filePath, line);
                    continue;
                }

                teams.Add(new Team
                {
                    Id = parts[0].Trim(),
                    Name = parts[1].Trim(),
                    BoardCode = parts[2].Trim()
                });
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error reading team data from {FilePath}", filePath);
            throw;
        }

        return teams;
    }
}
