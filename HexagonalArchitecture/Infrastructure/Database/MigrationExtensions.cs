namespace HexagonalArchitecture.Infrastructure.Database;

public static class MigrationExtensions
{
    public static IServiceCollection AddMigrationExtensions(this IServiceCollection services)
    {
        services.AddScoped<IDatabaseMigrator, DatabaseMigrator>();

        return services;
    }
}