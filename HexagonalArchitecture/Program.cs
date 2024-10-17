using HexagonalArchitecture.Adapter.Messaging.EventBus;
using HexagonalArchitecture.Adapter.Notification.Mock;
using HexagonalArchitecture.Adapter.Persistence.EntityFramework;
using HexagonalArchitecture.Application.Extensions;
using HexagonalArchitecture.Infrastructure;
using HexagonalArchitecture.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<UserContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection"), sqlOptions =>
    {
        sqlOptions.MigrationsHistoryTable("__EFMigrationsHistory", "dbo");
    }));

builder.Services.AddApplicationServices();
builder.Services.AddEventBusExtensions();
builder.Services.AddPersistenceEfExtensions();
builder.Services.AddMockEmailNotificationExtensions();
builder.Services.AddInfrastructureExtensions();
builder.Services.AddMigrationExtensions();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();