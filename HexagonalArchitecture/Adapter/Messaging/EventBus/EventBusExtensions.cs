using HexagonalArchitecture.Infrastructure.Adapter;
using HexagonalArchitecture.Infrastructure.Messaging;

namespace HexagonalArchitecture.Adapter.Messaging.EventBus;

public static class EventBusExtensions
{
    private const string AdapterSettingsSection = "Adapter";
    private const string EventBusValue = "EventBus";

    public static IServiceCollection AddEventBusAdapter(this IServiceCollection services, IConfiguration configuration)
    {
        AdapterSettings? settings = configuration.GetSection(AdapterSettingsSection).Get<AdapterSettings>();

        if (settings == null)
        {
            throw new InvalidOperationException($"Failed to load {AdapterSettingsSection} settings.");
        }

        if (EventBusValue != settings.Messaging) return services;

        services.AddSingleton<IMessagePublisher, EventBusMessagePublisher>();
        services.AddSingleton<IMessageHandler, EventBusIngestionHandler>();

        return services;
    }
}