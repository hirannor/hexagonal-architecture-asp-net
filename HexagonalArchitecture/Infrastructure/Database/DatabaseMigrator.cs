using HexagonalArchitecture.Adapter.Persistence.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace HexagonalArchitecture.Infrastructure.Database;

public class DatabaseMigrator(UserContext userDbContext) : IDatabaseMigrator
{
    public async Task MigrateAsync(CancellationToken cancellationToken)
    {
        await userDbContext.Database.MigrateAsync(cancellationToken);
    }
}