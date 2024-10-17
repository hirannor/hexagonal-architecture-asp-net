using HexagonalArchitecture.Infrastructure;

namespace HexagonalArchitecture.Adapter.Messaging.EventBus;

public static class EventBusExtensions
{
    public static IServiceCollection AddEventBusExtensions(this IServiceCollection services)
    {
        services.AddSingleton<IMessagePublisher, EventBusMessagePublisher>();
        services.AddSingleton<EventBusIngestionHandler>();
        services.AddSingleton<EventBusInitializer>();

        return services;
    }
}