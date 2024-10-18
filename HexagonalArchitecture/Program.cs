using HexagonalArchitecture.Adapter.Messaging.EventBus;
using HexagonalArchitecture.Adapter.Notification.Mock;
using HexagonalArchitecture.Adapter.Persistence.EntityFramework;
using HexagonalArchitecture.Adapter.Web.Rest.Filter;
using HexagonalArchitecture.Application.Extensions;
using HexagonalArchitecture.Infrastructure;
using HexagonalArchitecture.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers(options => { options.Filters.Add<ExceptionFilter>(); });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<UserContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddApplicationServices();
builder.Services.AddEventBusAdapter();
builder.Services.AddEntityFrameworkPersistenceAdapter();
builder.Services.AddMockEmailNotificationAdapter();
builder.Services.AddInfrastructureElements();
builder.Services.AddDatabaseMigrator();

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