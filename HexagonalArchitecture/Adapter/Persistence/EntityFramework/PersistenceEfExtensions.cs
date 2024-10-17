using HexagonalArchitecture.Domain;

namespace HexagonalArchitecture.Adapter.Persistence.EntityFramework;

public static class PersistenceEfExtensions
{
    public static IServiceCollection AddPersistenceEfExtensions(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserEfRepository>();

        return services;
    }
}