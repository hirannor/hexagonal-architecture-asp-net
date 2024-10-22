using HexagonalArchitecture.Application.UseCase;

namespace HexagonalArchitecture.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Application services
        services.AddScoped<ICustomerDisplay, CustomerService>();
        services.AddScoped<IChangePassword, CustomerPasswordService>();
        services.AddScoped<IChangeEmailAddress, CustomerService>();
        services.AddScoped<IChangePersonalDetails, CustomerService>();
        services.AddScoped<ICustomerRegistration, CustomerRegistrationService>();
        services.AddScoped<ICustomerSignIn, AuthenticationService>();
        services.AddScoped<IEventPublishing, EventPublisherService>();
        services.AddSingleton<INotificationSending, NotificationService>();

        return services;
    }
}