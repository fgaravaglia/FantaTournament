using System.Text.Json;
using FantaTournament.Domain.Entities;
using FantaTournament.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace FantaTournament.Infrastructure.Repositories;

/// <summary>
/// Implementation of <see cref="IForecastRepository"/> that persists data using JSON files.
/// </summary>
internal class JsonForecastRepository : IForecastRepository
{
    private readonly string _forecastsDirectory;
    private readonly ILogger<JsonForecastRepository> _logger;
    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true
    };

    /// <summary>
    /// Initializes a new instance of the <see cref="JsonForecastRepository"/> class.
    /// </summary>
    /// <param name="dataDirectory">The base directory where data files are stored.</param>
    /// <param name="logger">The logger instance.</param>
    public JsonForecastRepository(string dataDirectory, ILogger<JsonForecastRepository> logger)
    {
        _forecastsDirectory = Path.Combine(dataDirectory, "forecasts");
        _logger = logger;

        if (!Directory.Exists(_forecastsDirectory))
        {
            Directory.CreateDirectory(_forecastsDirectory);
        }
    }

    /// <inheritdoc/>
    public async Task<Forecast?> GetByIdAsync(string id)
    {
        var filePath = GetFilePath(id);

        if (!File.Exists(filePath))
        {
            return null;
        }

        try
        {
            var json = await File.ReadAllTextAsync(filePath);
            return JsonSerializer.Deserialize<Forecast>(json, _jsonOptions);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error reading forecast from {FilePath}", filePath);
            return null;
        }
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Forecast>> GetByUserIdAsync(string userId)
    {
        var forecasts = new List<Forecast>();

        try
        {
            var files = Directory.GetFiles(_forecastsDirectory, "FORECAST-*.json");
            foreach (var file in files)
            {
                var json = await File.ReadAllTextAsync(file);
                var forecast = JsonSerializer.Deserialize<Forecast>(json, _jsonOptions);
                if (forecast != null && forecast.UserId == userId)
                {
                    forecasts.Add(forecast);
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error reading forecasts for user {UserId}", userId);
        }

        return forecasts;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Forecast>> GetByBoardIdAsync(string boardId)
    {
        var forecasts = new List<Forecast>();

        try
        {
            var files = Directory.GetFiles(_forecastsDirectory, "FORECAST-*.json");
            foreach (var file in files)
            {
                var json = await File.ReadAllTextAsync(file);
                var forecast = JsonSerializer.Deserialize<Forecast>(json, _jsonOptions);
                if (forecast != null && forecast.BoardId == boardId)
                {
                    forecasts.Add(forecast);
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error reading forecasts for board {BoardId}", boardId);
        }

        return forecasts;
    }

    /// <inheritdoc/>
    public async Task UpdateAsync(Forecast forecast)
    {
        if (string.IsNullOrWhiteSpace(forecast.Id))
        {
            throw new ArgumentException("Forecast ID cannot be null or empty.", nameof(forecast));
        }

        var filePath = GetFilePath(forecast.Id);

        try
        {
            var json = JsonSerializer.Serialize(forecast, _jsonOptions);
            await File.WriteAllTextAsync(filePath, json);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving forecast to {FilePath}", filePath);
            throw;
        }
    }

    /// <inheritdoc/>
    public Task DeleteAsync(string id)
    {
        var filePath = GetFilePath(id);

        try
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting forecast file {FilePath}", filePath);
            throw;
        }

        return Task.CompletedTask;
    }

    private string GetFilePath(string id) => Path.Combine(_forecastsDirectory, $"FORECAST-{id}.json");
}
