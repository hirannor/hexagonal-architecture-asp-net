namespace HexagonalArchitecture.Infrastructure.Database;

public interface IDatabaseMigrator
{
    Task MigrateAsync(CancellationToken cancellationToken);
}