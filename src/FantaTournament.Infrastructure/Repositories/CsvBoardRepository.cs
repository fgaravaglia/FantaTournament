using FantaTournament.Domain.Entities;
using FantaTournament.Domain.Repositories;
using FantaTournament.Domain.ValueObjects;
using Microsoft.Extensions.Logging;
using System.Globalization;

namespace FantaTournament.Infrastructure.Repositories;

/// <summary>
/// Implementation of <see cref="IBoardRepository"/> that reads data from CSV files.
/// </summary>
internal class CsvBoardRepository : IBoardRepository
{
    private readonly string _dataDirectory;
    private readonly ITeamRepository _teamRepository;
    private readonly ILogger<CsvBoardRepository> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="CsvBoardRepository"/> class.
    /// </summary>
    /// <param name="dataDirectory">The directory where CSV files are stored.</param>
    /// <param name="teamRepository">The repository used to fetch teams.</param>
    /// <param name="logger">The logger instance.</param>
    public CsvBoardRepository(
        string dataDirectory, 
        ITeamRepository teamRepository, 
        ILogger<CsvBoardRepository> logger)
    {
        _dataDirectory = dataDirectory;
        _teamRepository = teamRepository;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<Board?> GetByIdAsync(string id)
    {
        var filePath = Path.Combine(_dataDirectory, $"BoardMatches_{id}.csv");

        if (!File.Exists(filePath))
        {
            _logger.LogWarning("Board matches CSV file not found: {FilePath}", filePath);
            return null;
        }

        // Read board metadata from BOARDS.csv
        var boardsFilePath = Path.Combine(_dataDirectory, "BOARDS.csv");
        string boardName = id;

        if (File.Exists(boardsFilePath))
        {
            var boardLines = await File.ReadAllLinesAsync(boardsFilePath);
            var boardMetadata = boardLines.Skip(1)
                .Select(l => l.Split(';'))
                .FirstOrDefault(p => p.Length >= 2 && p[0].Trim().Equals(id, StringComparison.OrdinalIgnoreCase));

            if (boardMetadata != null)
            {
                boardName = boardMetadata[1].Trim();
            }
        }

        var board = new Board
        {
            Id = id,
            Code = id,
            Name = boardName
        };

        try
        {
            // Load teams for this board using the team repository as requested
            var teamsList = (await _teamRepository.GetByBoardIdAsync(id)).ToList();
            board.Teams = teamsList;

            var lines = await File.ReadAllLinesAsync(filePath);
            // Header: Key;Container;TeamA;TeamB;MatchType;Date;NGoalA;NGoalB;NFinalA;NFinalB;IsStarted

            foreach (var line in lines.Skip(1))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                var parts = line.Split(';');
                if (parts.Length < 11) continue;

                var matchCode = parts[0].Trim();
                var teamAId = parts[2].Trim();
                var teamBId = parts[3].Trim();
                var matchType = parts[4].Trim();
                var dateStr = parts[5].Trim();
                var nGoalA = int.Parse(parts[6].Trim());
                var nGoalB = int.Parse(parts[7].Trim());
                var nFinalA = int.Parse(parts[8].Trim());
                var nFinalB = int.Parse(parts[9].Trim());
                var isStarted = parts[10].Trim() == "1";

                var match = new Match
                {
                    Id = matchCode,
                    Code = matchCode,
                    Phase = MapMatchTypeToPhase(matchType),
                    Date = DateTime.ParseExact(dateStr, "dd/MM/yyyy HH.mm", CultureInfo.InvariantCulture),
                    Status = isStarted ? MatchStatus.Played : MatchStatus.Scheduled,
                    HomeTeamId = teamAId,
                    AwayTeamId = teamBId,
                    HomeTeam = teamsList.FirstOrDefault(t => t.Id == teamAId),
                    AwayTeam = teamsList.FirstOrDefault(t => t.Id == teamBId),
                    HomeTeamPlaceholder = teamAId, // Use ID as placeholder if team not found
                    AwayTeamPlaceholder = teamBId,
                    Result = isStarted ? new MatchResult(
                        new Score(nGoalA, nGoalB),
                        (nFinalA != nGoalA || nFinalB != nGoalB) ? new Score(nFinalA, nFinalB) : null
                    ) : null
                };

                board.Matches.Add(match);
            }

            return board;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error reading board data from {FilePath}", filePath);
            throw;
        }
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Board>> SearchByNameAsync(string name)
    {
        var boardsFilePath = Path.Combine(_dataDirectory, "BOARDS.csv");
        if (!File.Exists(boardsFilePath))
        {
            _logger.LogWarning("BOARDS.csv not found at {FilePath}", boardsFilePath);
            return Enumerable.Empty<Board>();
        }

        try
        {
            var lines = await File.ReadAllLinesAsync(boardsFilePath);
            var results = new List<Board>();

            foreach (var line in lines.Skip(1))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                var parts = line.Split(';');
                if (parts.Length < 2) continue;

                var id = parts[0].Trim();
                var displayName = parts[1].Trim();

                if (displayName.Contains(name, StringComparison.OrdinalIgnoreCase))
                {
                    results.Add(new Board
                    {
                        Id = id,
                        Code = id,
                        Name = displayName
                    });
                }
            }

            return results;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching boards in {FilePath}", boardsFilePath);
            return Enumerable.Empty<Board>();
        }
    }

    /// <inheritdoc/>
    public Task UpdateAsync(Board board)
    {
        // CSV update is usually a rewrite of the whole file. 
        // For now, we omit implementation unless explicitly required to support writes.
        _logger.LogWarning("UpdateAsync not implemented for CsvBoardRepository");
        return Task.CompletedTask;
    }

    private static MatchPhase MapMatchTypeToPhase(string matchType)
    {
        return matchType.ToUpper() switch
        {
            "ROUND" => MatchPhase.GroupStage,
            "8TH" => MatchPhase.RoundOf16,
            "4TH" => MatchPhase.QuarterFinals,
            "SEMI-FINALS" => MatchPhase.SemiFinals,
            "FINAL12" => MatchPhase.Final1_2,
            _ => MatchPhase.GroupStage
        };
    }
}
