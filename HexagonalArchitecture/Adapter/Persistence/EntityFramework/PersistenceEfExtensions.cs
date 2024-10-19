using HexagonalArchitecture.Domain;
using HexagonalArchitecture.Infrastructure.Adapter;

namespace HexagonalArchitecture.Adapter.Persistence.EntityFramework;

public static class PersistenceEfExtensions
{
    private const string Adapter = "Adapter";
    private const string EntityFramework = "EntityFramework";

    public static IServiceCollection AddEntityFrameworkPersistenceAdapter(this IServiceCollection services, IConfiguration configuration)
    {
        var adapterSettings = configuration.GetSection(Adapter).Get<AdapterSettings>();

        if (EntityFramework == adapterSettings.Persistence)
        {
            services.AddScoped<IUserRepository, UserEfRepository>();
        }
        
        return services;
    }
}