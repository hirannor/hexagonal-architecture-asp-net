using System.Configuration;
using HexagonalArchitecture.Application.Port;
using HexagonalArchitecture.Infrastructure.Adapter;

namespace HexagonalArchitecture.Adapter.Notification.Email;

public static class EmailNotificationExtensions
{
    private const string Adapter = "Adapter";
    private const string Email = "Email";
    private const string EmailSettings = "EmailSettings";

    public static IServiceCollection AddEmailNotificationAdapter(this IServiceCollection services,
        IConfiguration configuration)
    {
        var adapterSettings = configuration.GetSection(Adapter).Get<AdapterSettings>();

        if (Email == adapterSettings.Notification)
        {
            services.AddTransient<IEmailNotification, EmailNotification>();
            services.Configure<EmailSettings>(configuration.GetSection(EmailSettings));
        }
        
        return services;
    }
}