using Microsoft.EntityFrameworkCore;

namespace HexagonalArchitecture.Adapter.Persistence.EntityFramework;

public class HexagonDbContext(DbContextOptions<HexagonDbContext> options) : DbContext(options)
{
    public DbSet<CustomerModel> Users { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CustomerModel>()
            .HasIndex(u => u.EmailAddress)
            .IsUnique();

        modelBuilder.Entity<CustomerModel>()
            .HasIndex(u => u.CustomerId)
            .IsUnique();

        modelBuilder.Entity<CustomerModel>()
            .HasIndex(u => u.Username)
            .IsUnique();
    }
}