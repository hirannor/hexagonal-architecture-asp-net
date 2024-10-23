using HexagonalArchitecture.Domain;
using HexagonalArchitecture.Infrastructure.Adapter;
using Microsoft.EntityFrameworkCore;

namespace HexagonalArchitecture.Adapter.Persistence.EntityFramework;

public static class PersistenceEfExtensions
{
    private const string AdapterSettingsSection = "Adapter";
    private const string EntityFramework = "EntityFramework";
    private const string ConnectionString = "DefaultConnection";

    public static IServiceCollection AddEntityFrameworkPersistenceAdapter(this IServiceCollection services,
        IConfiguration configuration)
    {
        AdapterSettings? settings = configuration.GetSection(AdapterSettingsSection).Get<AdapterSettings>();

        if (settings == null)
        {
            throw new InvalidOperationException($"Failed to load {AdapterSettingsSection} settings.");
        }

        if (EntityFramework != settings.Persistence) return services;

        services.AddScoped<ICustomerRepository, CustomerEfRepository>();

        services.AddDbContext<HexagonDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString(ConnectionString)));

        return services;
    }
}