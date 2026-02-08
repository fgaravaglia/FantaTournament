using FantaTournament.Application.Commands;
using FantaTournament.Application.Queries;
using FantaTournament.Application.Subscribers;
using FantaTournament.Domain.Events;
using FantaTournament.Domain.Services;
using FantaTournament.Domain.Services.Scoring;
using Microsoft.Extensions.DependencyInjection;
using Umbrella.Core.Messaging;

namespace FantaTournament.Application;

/// <summary>
/// Provides extension methods for registering application layer services.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds the application services to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The service collection to add the services to.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Commands
        services.AddScoped<IBoardCommands, BoardCommands>();
        services.AddScoped<IForecastCommands, ForecastCommands>();

        // Queries
        services.AddScoped<IForecastQueries, ForecastQueries>();

        // Domain Services (if not registered elsewhere)
        services.AddScoped<IScoringPolicy, ScoringService>();
        services.AddScoped<RankingService>();

        // Subscribers (Event Handlers)
        services.AddScoped<IEventHandler<MatchResultUpdatedEvent>, ForecastRecalculator>();
        services.AddScoped<IEventHandler<MatchResultUpdatedEvent>, RankingRecalculator>();

        return services;
    }
}
