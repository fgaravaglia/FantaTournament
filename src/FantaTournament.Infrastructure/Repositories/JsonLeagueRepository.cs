using System.Text.Json;
using FantaTournament.Domain.Entities;
using FantaTournament.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace FantaTournament.Infrastructure.Repositories;

/// <summary>
/// Implementation of <see cref="ILeagueRepository"/> that persists data using JSON files.
/// </summary>
internal class JsonLeagueRepository : ILeagueRepository
{
    private readonly string _leaguesDirectory;
    private readonly ILogger<JsonLeagueRepository> _logger;
    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true
    };

    /// <summary>
    /// Initializes a new instance of the <see cref="JsonLeagueRepository"/> class.
    /// </summary>
    /// <param name="dataDirectory">The base directory where data files are stored.</param>
    /// <param name="logger">The logger instance.</param>
    public JsonLeagueRepository(string dataDirectory, ILogger<JsonLeagueRepository> logger)
    {
        _leaguesDirectory = Path.Combine(dataDirectory, "leagues");
        _logger = logger;

        if (!Directory.Exists(_leaguesDirectory))
        {
            Directory.CreateDirectory(_leaguesDirectory);
        }
    }

    /// <inheritdoc/>
    public async Task<League?> GetByIdAsync(string id)
    {
        var filePath = GetFilePath(id);

        if (!File.Exists(filePath))
        {
            return null;
        }

        try
        {
            var json = await File.ReadAllTextAsync(filePath);
            return JsonSerializer.Deserialize<League>(json, _jsonOptions);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error reading league from {FilePath}", filePath);
            return null;
        }
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<League>> GetByBoardIdAsync(string boardId)
    {
        var leagues = new List<League>();

        try
        {
            var files = Directory.GetFiles(_leaguesDirectory, "LEAGUE-*.json");
            foreach (var file in files)
            {
                var json = await File.ReadAllTextAsync(file);
                var league = JsonSerializer.Deserialize<League>(json, _jsonOptions);
                if (league != null && league.BoardId == boardId)
                {
                    leagues.Add(league);
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error reading leagues for board {BoardId}", boardId);
        }

        return leagues;
    }

    /// <inheritdoc/>
    public async Task UpdateAsync(League league)
    {
        if (string.IsNullOrWhiteSpace(league.Id))
        {
            throw new ArgumentException("League ID cannot be null or empty.", nameof(league));
        }

        var filePath = GetFilePath(league.Id);

        try
        {
            var json = JsonSerializer.Serialize(league, _jsonOptions);
            await File.WriteAllTextAsync(filePath, json);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving league to {FilePath}", filePath);
            throw;
        }
    }

    private string GetFilePath(string id) => Path.Combine(_leaguesDirectory, $"LEAGUE-{id}.json");
}
