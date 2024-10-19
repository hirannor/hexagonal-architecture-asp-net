using HexagonalArchitecture.Infrastructure.Adapter;
using HexagonalArchitecture.Infrastructure.Messaging;

namespace HexagonalArchitecture.Adapter.Messaging.EventBus;

public static class EventBusExtensions
{
    private const string Adapter = "Adapter";
    private const string EventBus = "EventBus";

    public static IServiceCollection AddEventBusAdapter(this IServiceCollection services, IConfiguration configuration)
    {
        var adapterSettings = configuration.GetSection(Adapter).Get<AdapterSettings>();

        if (EventBus != adapterSettings.Messaging) return services;
        
        services.AddSingleton<IMessagePublisher, EventBusMessagePublisher>();
        services.AddSingleton<IMessageHandler, EventBusIngestionHandler>();

        return services;
    }
}