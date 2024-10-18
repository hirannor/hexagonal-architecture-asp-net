using HexagonalArchitecture.Infrastructure.Messaging;

namespace HexagonalArchitecture.Adapter.Messaging.EventBus;

public static class EventBusExtensions
{
    public static IServiceCollection AddEventBusAdapter(this IServiceCollection services)
    {
        services.AddSingleton<IMessagePublisher, EventBusMessagePublisher>();
        services.AddSingleton<IMessageHandler, EventBusIngestionHandler>();

        return services;
    }
}