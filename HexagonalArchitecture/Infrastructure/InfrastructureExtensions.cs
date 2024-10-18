using HexagonalArchitecture.Infrastructure.Eventing;

namespace HexagonalArchitecture.Infrastructure;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructureElements(this IServiceCollection services)
    {
        // Application services
        services.AddHostedService<StartupHostedService>();
        services.AddSingleton<EventBusInitializer>();

        return services;
    }
}