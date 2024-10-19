using System.Data.Common;
using HexagonalArchitecture.Adapter.Persistence.EntityFramework;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetWebApi.Tests;

public class CustomWebApplicationFactory(SqlServerContainerFixture fixture) : WebApplicationFactory<Program>
{
    private readonly string _connectionString = fixture.ConnectionString;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.Remove(services.SingleOrDefault(service => typeof(DbContextOptions<HexagonDbContext>) == service.ServiceType));
            services.Remove(services.SingleOrDefault(service => typeof(DbConnection) == service.ServiceType));
            services.AddDbContext<HexagonDbContext>((_, option) => option.UseSqlServer(_connectionString));
        });
    }
}