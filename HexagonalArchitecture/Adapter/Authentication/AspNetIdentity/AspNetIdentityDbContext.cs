using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HexagonalArchitecture.Adapter.Authentication.AspNetIdentity
{
    public class AspNetIdentityDbContext(DbContextOptions<AspNetIdentityDbContext> options)
        : IdentityDbContext<ApplicationUserModel>(options);
}