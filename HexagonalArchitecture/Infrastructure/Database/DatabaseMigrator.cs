using HexagonalArchitecture.Adapter.Authentication.AspNetIdentity;
using HexagonalArchitecture.Adapter.Persistence.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace HexagonalArchitecture.Infrastructure.Database;

public class DatabaseMigrator(CustomersDbContext customersDbDbContext, AspNetIdentityDbContext aspNetIdentityDbContext)
    : IDatabaseMigrator
{
    public async Task MigrateAsync(CancellationToken cancellationToken)
    {
        await customersDbDbContext.Database.MigrateAsync(cancellationToken);
        await aspNetIdentityDbContext.Database.MigrateAsync(cancellationToken);
    }
}