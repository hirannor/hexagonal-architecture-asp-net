﻿using System.Data.Common;
using HexagonalArchitecture.Adapter.Authentication.AspNetIdentity;
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
            services.Remove(services.SingleOrDefault(service =>
                typeof(DbContextOptions<CustomersDbContext>) == service.ServiceType));
            services.Remove(services.SingleOrDefault(service =>
                typeof(DbContextOptions<AspNetIdentityDbContext>) == service.ServiceType));
            services.Remove(services.SingleOrDefault(service => typeof(DbConnection) == service.ServiceType));
            services.AddDbContext<CustomersDbContext>((_, option) => option.UseSqlServer(_connectionString));
            services.AddDbContext<AspNetIdentityDbContext>((_, option) => option.UseSqlServer(_connectionString));
        });
    }
}