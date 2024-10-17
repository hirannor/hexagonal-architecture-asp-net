using HexagonalArchitecture.Application.UseCase;

namespace HexagonalArchitecture.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Application services
        services.AddScoped<IUserCreation, UserManagementService>();
        services.AddScoped<IUserDisplay, UserManagementService>();
        services.AddScoped<IUserDeletion, UserManagementService>();
        services.AddScoped<IEventPublishing, EventPublisherService>();

        return services;
    }
}