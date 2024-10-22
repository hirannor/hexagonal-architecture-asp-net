using System.Text;
using HexagonalArchitecture.Application.Port;
using HexagonalArchitecture.Infrastructure.Adapter;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace HexagonalArchitecture.Adapter.Authentication.AspNetIdentity;

public static class AspNetIdentityAuthenticationExtensions
{
    private const string AdapterSettingsSection = "Adapter";
    private const string AuthenticationValue = "AspNetIdentity";

    public static IServiceCollection AddAspNetIdentityAuthenticationAdapter(this IServiceCollection services,
        IConfiguration configuration)
    {
        var adapterSettings = configuration.GetSection(AdapterSettingsSection).Get<AdapterSettings>();

        if (adapterSettings == null)
        {
            throw new InvalidOperationException($"Failed to load {AdapterSettingsSection} settings.");
        }

        if (AuthenticationValue != adapterSettings.Authentication) return services;

        var jwtSettings = configuration.GetSection("Jwt");
        var issuer = jwtSettings["Issuer"];
        var audience = jwtSettings["Audience"];
        var key = jwtSettings["Key"];

        services.AddDbContext<AspNetIdentityDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddIdentity<ApplicationUserModel, IdentityRole>()
            .AddEntityFrameworkStores<AspNetIdentityDbContext>()
            .AddDefaultTokenProviders();

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                };
            });
        services.AddAuthorization();

        services.AddScoped<IAuthentication, AspNetIdentityAuthentication>();

        return services;
    }
}