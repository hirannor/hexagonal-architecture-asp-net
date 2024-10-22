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
        var adapterSettings = configuration.GetSection(AdapterSettingsSection).Get<AdapterSettings>();

        if (adapterSettings == null)
        {
            throw new InvalidOperationException($"Failed to load {AdapterSettingsSection} settings.");
        }

        if (EmailValue != adapterSettings.Notification) return services;

        services.AddTransient<IEmailNotification, EmailNotification>();
        services.Configure<EmailSettings>(configuration.GetSection(EmailSettings));

        return services;
    }
}