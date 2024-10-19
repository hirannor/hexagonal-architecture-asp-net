using HexagonalArchitecture.Adapter.Persistence.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace HexagonalArchitecture.Infrastructure.Database;

public class DatabaseMigrator(HexagonDbContext hexagonDbDbContext) : IDatabaseMigrator
{
    public async Task MigrateAsync(CancellationToken cancellationToken)
    {
        await hexagonDbDbContext.Database.MigrateAsync(cancellationToken);
    }
}