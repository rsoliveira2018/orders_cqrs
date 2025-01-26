using Core.Interfaces.Repositories.Commands;
using Core.Interfaces.Repositories.Queries;
using Core.Interfaces.Repositories.Services;
using Core.Services;
using Infrastructure.Data;
using Infrastructure.Mongo;
using Infrastructure.Repositories.Commands;
using Infrastructure.Repositories.Queries;
using Infrastructure.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using OrdersCQRS.Handlers.Commands;
using OrdersCQRS.Handlers.Queries;

var builder = WebApplication.CreateBuilder(args);

ConfigureMongoDB(builder);

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

InjectRepositories(builder);
InjectServices(builder);
InjectCommandsAndQueries(builder);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

SeedSQLServerData(app);
SeedMongoData(app);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static void SeedSQLServerData(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        Infrastructure.Seed.SQLServerSeedData.Initialize(context);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error seeding SQL Server data: {ex.Message}");
    }
}

static void SeedMongoData(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    try
    {
        var database = scope.ServiceProvider.GetRequiredService<IMongoDatabase>();
        MongoSeedData.Seed(database);
    } 
    catch (Exception ex)
    {
        Console.WriteLine($"Error seeding MongoDB data: {ex.Message}");
    }

}

static void InjectRepositories(WebApplicationBuilder builder)
{
    builder.Services.AddScoped<IProductCommandRepository, ProductCommandRepository>();
    builder.Services.AddScoped<ICustomerCommandRepository, CustomerCommandRepository>();
    builder.Services.AddScoped<IOrderCommandRepository, OrderCommandRepository>();
    builder.Services.AddScoped<IOrderItemCommandRepository, OrderItemCommandRepository>();

    builder.Services.AddScoped<IProductQueryRepository, ProductQueryRepository>();
    builder.Services.AddScoped<ICustomerQueryRepository, CustomerQueryRepository>();
    builder.Services.AddScoped<IOrderQueryRepository, OrderQueryRepository>();
    builder.Services.AddScoped<IOrderItemQueryRepository, OrderItemQueryRepository>();
}

static void InjectServices(WebApplicationBuilder builder)
{
    builder.Services.AddScoped<IOrderService, OrderService>();
}

static void InjectCommandsAndQueries(WebApplicationBuilder builder)
{
    builder.Services.AddScoped<ProductCommandHandler>();
    builder.Services.AddScoped<ProductQueryHandler>();

    builder.Services.AddScoped<CustomerCommandHandler>();
    builder.Services.AddScoped<CustomerQueryHandler>();

    builder.Services.AddScoped<OrderCommandHandler>();
    builder.Services.AddScoped<OrderQueryHandler>();

    builder.Services.AddScoped<OrderItemCommandHandler>();
    builder.Services.AddScoped<OrderItemQueryHandler>();
}

static void ConfigureMongoDB(WebApplicationBuilder builder)
{
    builder.Services.Configure<MongoSettings>(builder.Configuration.GetSection("MongoSettings"));
    builder.Services.AddSingleton<IMongoClient, MongoClient>(sp =>
    {
        var settings = sp.GetRequiredService<IOptions<MongoSettings>>().Value;
        return new MongoClient(settings.ConnectionString);
    });
    builder.Services.AddScoped(sp =>
    {
        var settings = sp.GetRequiredService<IOptions<MongoSettings>>().Value;
        var client = sp.GetRequiredService<IMongoClient>();
        return client.GetDatabase(settings.DatabaseName);
    });
}