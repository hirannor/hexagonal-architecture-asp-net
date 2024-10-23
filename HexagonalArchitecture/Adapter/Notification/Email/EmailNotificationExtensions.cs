using HexagonalArchitecture.Application.Port;
using HexagonalArchitecture.Infrastructure.Adapter;

namespace HexagonalArchitecture.Adapter.Notification.Email;

public static class EmailNotificationExtensions
{
    private const string AdapterSettingsSection = "Adapter";
    private const string EmailValue = "Email";
    private const string EmailSettings = "EmailSettings";

    public static IServiceCollection AddEmailNotificationAdapter(this IServiceCollection services,
        IConfiguration configuration)
    {
        AdapterSettings? settings = configuration.GetSection(AdapterSettingsSection).Get<AdapterSettings>();

        if (settings == null)
        {
            throw new InvalidOperationException($"Failed to load {AdapterSettingsSection} settings.");
        }

        if (EmailValue != settings.Notification) return services;

        services.AddTransient<IEmailNotification, EmailNotification>();
        services.Configure<EmailSettings>(configuration.GetSection(EmailSettings));

        return services;
    }
}