using Microsoft.EntityFrameworkCore;

namespace HexagonalArchitecture.Adapter.Persistence.EntityFramework
{
    public class UserContext(DbContextOptions<UserContext> options) : DbContext(options)
    {
        public DbSet<UserModel> Users { get; set; }
    }
}