using HexagonalArchitecture.Domain;

namespace HexagonalArchitecture.Adapter.Persistence.EntityFramework;

public static class PersistenceEfExtensions
{
    public static IServiceCollection AddEntityFrameworkPersistenceAdapter(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserEfRepository>();

        return services;
    }
}