using Umbrella.Core.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace Umbrella.Core;

public static class DependencyInjection
{
    /// <summary>
    /// Registers all business-related dependencies (Domain, Application, Infrastructure, Messaging).
    /// </summary>
    public static IServiceCollection AddUmbrellaCore(this IServiceCollection services)
    {
        services.AddSingleton<IEventBus, InMemoryEventBus>();
        return services;
    }
}
