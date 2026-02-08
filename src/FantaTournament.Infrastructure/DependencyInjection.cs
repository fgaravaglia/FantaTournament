using FantaTournament.Domain.Repositories;
using FantaTournament.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FantaTournament.Infrastructure;

/// <summary>
/// Provides extension methods for registering infrastructure services.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds the infrastructure services to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The service collection to add the services to.</param>
    /// <param name="configuration">The configuration to use.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        return services;
    }

    /// <summary>
    /// Adds the capability to store boards and teams data using CSV files to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The service collection to add the services to.</param>
    /// <param name="configuration">The configuration to use.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection UsingCsvPersistenceForBoards(this IServiceCollection services, IConfiguration configuration)
    {
        // For now, we use a hardcoded relative path to the data folder or retrieve it from configuration
        var dataPath = configuration["Persistence:CsvDataPath"] ?? "data";
        
        // Ensure the path is absolute for reliability
        var absoluteDataPath = Path.IsPathRooted(dataPath) 
            ? dataPath 
            : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dataPath);

        services.AddScoped<ITeamRepository>(sp => 
            new CsvTeamRepository(absoluteDataPath, sp.GetRequiredService<ILogger<CsvTeamRepository>>()));

        services.AddScoped<IBoardRepository>(sp => 
            new CsvBoardRepository(
                absoluteDataPath, 
                sp.GetRequiredService<ITeamRepository>(), 
                sp.GetRequiredService<ILogger<CsvBoardRepository>>()));

        return services;
    }


    /// <summary>
    /// Adds the capability to store forecasts data using JSON files to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The service collection to add the services to.</param>
    /// <param name="configuration">The configuration to use.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection UsingJsonPersistenceForForecasts(this IServiceCollection services, IConfiguration configuration)
    {
        var dataPath = configuration["Persistence:JsonDataPath"] ?? "data";

        var absoluteDataPath = Path.IsPathRooted(dataPath)
            ? dataPath
            : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dataPath);

        services.AddScoped<IForecastRepository>(sp =>
            new JsonForecastRepository(absoluteDataPath, sp.GetRequiredService<ILogger<JsonForecastRepository>>()));

        services.AddScoped<ILeagueRepository>(sp =>
            new JsonLeagueRepository(absoluteDataPath, sp.GetRequiredService<ILogger<JsonLeagueRepository>>()));

        services.AddScoped<IRankingRepository>(sp =>
            new JsonRankingRepository(absoluteDataPath, sp.GetRequiredService<ILogger<JsonRankingRepository>>()));

        return services;
    }
}
