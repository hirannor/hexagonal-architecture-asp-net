namespace HexagonalArchitecture.Infrastructure.Database;

public static class MigrationExtensions
{
    public static IServiceCollection AddDatabaseMigrator(this IServiceCollection services)
    {
        services.AddScoped<IDatabaseMigrator, DatabaseMigrator>();

        return services;
    }
}