using System.Text.Json;
using FantaTournament.Domain.Entities;
using FantaTournament.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace FantaTournament.Infrastructure.Repositories;

/// <summary>
/// Implementation of <see cref="IRankingRepository"/> that persists data using JSON files.
/// </summary>
internal class JsonRankingRepository : IRankingRepository
{
    private readonly string _rankingsDirectory;
    private readonly ILogger<JsonRankingRepository> _logger;
    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true
    };

    /// <summary>
    /// Initializes a new instance of the <see cref="JsonRankingRepository"/> class.
    /// </summary>
    /// <param name="dataDirectory">The base directory where data files are stored.</param>
    /// <param name="logger">The logger instance.</param>
    public JsonRankingRepository(string dataDirectory, ILogger<JsonRankingRepository> logger)
    {
        _rankingsDirectory = Path.Combine(dataDirectory, "rankings");
        _logger = logger;

        if (!Directory.Exists(_rankingsDirectory))
        {
            Directory.CreateDirectory(_rankingsDirectory);
        }
    }

    /// <inheritdoc/>
    public async Task<Ranking?> GetAsync(string boardId, string? leagueId = null)
    {
        var filePath = GetFilePath(boardId, leagueId);

        if (!File.Exists(filePath))
        {
            return null;
        }

        try
        {
            var json = await File.ReadAllTextAsync(filePath);
            return JsonSerializer.Deserialize<Ranking>(json, _jsonOptions);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error reading ranking from {FilePath}", filePath);
            return null;
        }
    }

    /// <inheritdoc/>
    public async Task UpdateAsync(Ranking ranking)
    {
        if (string.IsNullOrWhiteSpace(ranking.BoardId))
        {
            throw new ArgumentException("Ranking BoardId cannot be null or empty.", nameof(ranking));
        }

        var filePath = GetFilePath(ranking.BoardId, ranking.LeagueId);

        try
        {
            var json = JsonSerializer.Serialize(ranking, _jsonOptions);
            await File.WriteAllTextAsync(filePath, json);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving ranking to {FilePath}", filePath);
            throw;
        }
    }

    private string GetFilePath(string boardId, string? leagueId)
    {
        var fileName = string.IsNullOrWhiteSpace(leagueId) 
            ? $"RANKING-{boardId}-GLOBAL.json"
            : $"RANKING-{boardId}-LEAGUE-{leagueId}.json";
            
        return Path.Combine(_rankingsDirectory, fileName);
    }
}
