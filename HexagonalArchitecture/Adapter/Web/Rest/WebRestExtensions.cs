using HexagonalArchitecture.Adapter.Web.Rest.Filter;
using HexagonalArchitecture.Infrastructure.Adapter;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace HexagonalArchitecture.Adapter.Web.Rest;

public static class WebRestExtensions
{
    private const string AdapterSettingsSection = "Adapter";
    private const string RestValue = "Rest";

    public static IServiceCollection AddWebRestAdapter(this IServiceCollection services, IConfiguration configuration)
    {
        var adapterSettings = configuration.GetSection(AdapterSettingsSection).Get<AdapterSettings>();

        if (adapterSettings == null)
        {
            throw new InvalidOperationException($"Failed to load {AdapterSettingsSection} settings.");
        }
        
        if (RestValue != adapterSettings.Web) return services;
        
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Please enter your token in the format ** {your token} **",
                Name = "Authentication",
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                Type = SecuritySchemeType.Http
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        services.AddSingleton<JwtTokenGenerator>();
        services.AddControllers(options => { options.Filters.Add<ExceptionFilter>(); });

        return services;
    }
}