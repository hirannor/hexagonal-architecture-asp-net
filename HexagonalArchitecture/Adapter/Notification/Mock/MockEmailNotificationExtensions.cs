using HexagonalArchitecture.Application.Port;
using HexagonalArchitecture.Infrastructure.Adapter;

namespace HexagonalArchitecture.Adapter.Notification.Mock;

public static class MockEmailNotificationExtensions
{
    private const string Adapter = "Adapter";
    private const string Mock = "Mock";

    public static IServiceCollection AddMockEmailNotificationAdapter(this IServiceCollection services,
        IConfiguration configuration)
    {
        var adapterSettings = configuration.GetSection(Adapter).Get<AdapterSettings>();

        if (Mock == adapterSettings.Notification)
        {
            services.AddSingleton<IEmailNotification, MockEmailNotification>();
        }
        
        return services;
    }
}