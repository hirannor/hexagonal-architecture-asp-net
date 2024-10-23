using HexagonalArchitecture.Domain;
using HexagonalArchitecture.Infrastructure.Adapter;

namespace HexagonalArchitecture.Adapter.Persistence.InMemory;

public static class InMemoryExtensions
{
    private const string AdapterSettingsSection = "Adapter";
    private const string InMemory = "InMemory";

    public static IServiceCollection AddInMemoryPersistenceAdapter(this IServiceCollection services,
        IConfiguration configuration)
    {
        AdapterSettings? settings = configuration.GetSection(AdapterSettingsSection).Get<AdapterSettings>();

        if (settings == null)
        {
            throw new InvalidOperationException($"Failed to load {AdapterSettingsSection} settings.");
        }

        if (InMemory == settings.Persistence)
        {
            services.AddScoped<ICustomerRepository, CustomerInMemoryRepository>();
        }

        return services;
    }
}