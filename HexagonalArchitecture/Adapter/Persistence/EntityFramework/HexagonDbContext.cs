using Microsoft.EntityFrameworkCore;

namespace HexagonalArchitecture.Adapter.Persistence.EntityFramework;

public class HexagonDbContext(DbContextOptions<HexagonDbContext> options) : DbContext(options)
{
    public DbSet<UserModel> Users { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserModel>()
            .HasIndex(u => u.EmailAddress)
            .IsUnique();

        modelBuilder.Entity<UserModel>()
            .HasIndex(u => u.UserId)
            .IsUnique();
    }
}