using HexagonalArchitecture.Domain;
using HexagonalArchitecture.Infrastructure.Adapter;

namespace HexagonalArchitecture.Adapter.Persistence.InMemory;

public static class InMemoryExtensions
{
    private const string Adapter = "Adapter";
    private const string InMemory = "InMemory";

    public static IServiceCollection AddInMemoryPersistenceAdapter(this IServiceCollection services, IConfiguration configuration)
    {
        var adapterSettings = configuration.GetSection(Adapter).Get<AdapterSettings>();

        if (InMemory == adapterSettings.Persistence)
        {
            services.AddScoped<IUserRepository, UserInMemoryRepository>();
        }
        
        return services;
    }
}