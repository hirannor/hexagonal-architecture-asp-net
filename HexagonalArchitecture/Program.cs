using HexagonalArchitecture.Adapter.Authentication.AspNetIdentity;
using HexagonalArchitecture.Adapter.Messaging.EventBus;
using HexagonalArchitecture.Adapter.Notification.Email;
using HexagonalArchitecture.Adapter.Notification.Mock;
using HexagonalArchitecture.Adapter.Persistence.EntityFramework;
using HexagonalArchitecture.Adapter.Persistence.InMemory;
using HexagonalArchitecture.Adapter.Web.Rest;
using HexagonalArchitecture.Application.Extensions;
using HexagonalArchitecture.Infrastructure;
using HexagonalArchitecture.Infrastructure.Database;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Add application services
builder.Services.AddApplicationServices();

// Add adapters
builder.Services.AddWebRestAdapter(builder.Configuration);
builder.Services.AddEventBusAdapter(builder.Configuration);
builder.Services.AddEntityFrameworkPersistenceAdapter(builder.Configuration);
builder.Services.AddInMemoryPersistenceAdapter(builder.Configuration);
builder.Services.AddMockEmailNotificationAdapter(builder.Configuration);
builder.Services.AddEmailNotificationAdapter(builder.Configuration);
builder.Services.AddAspNetIdentityAuthenticationAdapter(builder.Configuration);

// Add infrastructure elements and database migrator
builder.Services.AddInfrastructureElements();
builder.Services.AddDatabaseMigrator();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

public partial class Program
{
}