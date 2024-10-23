using HexagonalArchitecture.Application.Port;
using HexagonalArchitecture.Infrastructure.Adapter;

namespace HexagonalArchitecture.Adapter.Notification.Mock;

public static class MockEmailNotificationExtensions
{
    private const string AdapterSettingsSection = "Adapter";
    private const string MockValue = "Mock";

    public static IServiceCollection AddMockEmailNotificationAdapter(this IServiceCollection services,
        IConfiguration configuration)
    {
        AdapterSettings? settings = configuration.GetSection(AdapterSettingsSection).Get<AdapterSettings>();

        if (settings == null)
        {
            throw new InvalidOperationException($"Failed to load {AdapterSettingsSection} settings.");
        }

        if (MockValue == settings.Notification)
        {
            services.AddSingleton<IEmailNotification, MockEmailNotification>();
        }

        return services;
    }
}