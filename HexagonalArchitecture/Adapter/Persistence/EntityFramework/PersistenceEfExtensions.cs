using HexagonalArchitecture.Domain;
using HexagonalArchitecture.Infrastructure.Adapter;
using Microsoft.EntityFrameworkCore;

namespace HexagonalArchitecture.Adapter.Persistence.EntityFramework;

public static class PersistenceEfExtensions
{
    private const string AdapterSettingsSection = "Adapter";
    private const string EntityFramework = "EntityFramework";

    public static IServiceCollection AddEntityFrameworkPersistenceAdapter(this IServiceCollection services, IConfiguration configuration)
    {
        var adapterSettings = configuration.GetSection(AdapterSettingsSection).Get<AdapterSettings>();

        if (EntityFramework != adapterSettings.Persistence) return services;
        
        services.AddScoped<IUserRepository, UserEfRepository>();
            
        services.AddDbContext<HexagonDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        return services;
    }
}