using FantaTournament.Application;
using FantaTournament.Infrastructure;
using Umbrella.Core;

namespace FantaTournament.Api.Extensions;

/// <summary>
/// Extension methods for registering FantaTournament business dependencies.
/// </summary>
public static class FantaTournamentExtensions
{
    /// <summary>
    /// Registers all business-related dependencies (Domain, Application, Infrastructure, Messaging).
    /// </summary>
    public static IServiceCollection AddFantaTournament(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Register Application Layer (Commands, Queries, Subscribers)
        services.AddApplication();

        // Register Infrastructure Layer (Persistence)
        services.UsingCsvPersistenceForBoards(configuration)
                .UsingJsonPersistenceForForecasts(configuration);

        return services;
    }
}
