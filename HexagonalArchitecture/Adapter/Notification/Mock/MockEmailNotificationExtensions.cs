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
        var adapterSettings = configuration.GetSection(AdapterSettingsSection).Get<AdapterSettings>();

        if (adapterSettings == null)
        {
            throw new InvalidOperationException($"Failed to load {AdapterSettingsSection} settings.");
        }
        
        if (MockValue == adapterSettings.Notification)
        {
            services.AddSingleton<IEmailNotification, MockEmailNotification>();
        }
        
        return services;
    }
}