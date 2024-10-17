using HexagonalArchitecture.Application.Port;

namespace HexagonalArchitecture.Adapter.Notification.Mock;

public static class MockEmailNotificationExtensions
{
    public static IServiceCollection AddMockEmailNotificationExtensions(this IServiceCollection services)
    {
        services.AddSingleton<IEmailNotification, MockEmailNotification>();

        return services;
    }
}