using HexagonalArchitecture.Domain;
using HexagonalArchitecture.Infrastructure.Adapter;
using Microsoft.EntityFrameworkCore;

namespace HexagonalArchitecture.Adapter.Persistence.EntityFramework;

public static class PersistenceEfExtensions
{
    private const string AdapterSettingsSection = "Adapter";
    private const string EntityFramework = "EntityFramework";

    public static IServiceCollection AddEntityFrameworkPersistenceAdapter(this IServiceCollection services,
        IConfiguration configuration)
    {
        var adapterSettings = configuration.GetSection(AdapterSettingsSection).Get<AdapterSettings>();

        if (adapterSettings == null)
        {
            throw new InvalidOperationException($"Failed to load {AdapterSettingsSection} settings.");
        }

        if (EntityFramework != adapterSettings.Persistence) return services;

        services.AddScoped<ICustomerRepository, CustomerEfRepository>();

        services.AddDbContext<HexagonDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        return services;
    }
}