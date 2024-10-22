using HexagonalArchitecture.Infrastructure.Adapter;
using HexagonalArchitecture.Infrastructure.Messaging;

namespace HexagonalArchitecture.Adapter.Messaging.EventBus;

public static class EventBusExtensions
{
    private const string AdapterSettingsSection = "Adapter";
    private const string EventBusValue = "EventBus";

    public static IServiceCollection AddEventBusAdapter(this IServiceCollection services, IConfiguration configuration)
    {
        var adapterSettings = configuration.GetSection(AdapterSettingsSection).Get<AdapterSettings>();

        if (adapterSettings == null)
        {
            throw new InvalidOperationException($"Failed to load {AdapterSettingsSection} settings.");
        }

        if (EventBusValue != adapterSettings.Messaging) return services;

        services.AddSingleton<IMessagePublisher, EventBusMessagePublisher>();
        services.AddSingleton<IMessageHandler, EventBusIngestionHandler>();

        return services;
    }
}