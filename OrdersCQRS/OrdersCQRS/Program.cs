using Core.Interfaces;
using Core.Services;
using Infrastructure.Data;
using Infrastructure.Mongo;
using Infrastructure.Repositories;
using Infrastructure.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

ConfigureMongoDB(builder);

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

InjectRepositories(builder);

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
    builder.Services.AddScoped<IProductReadRepository, ProductReadRepository>();
    builder.Services.AddScoped<IOrderReadRepository, OrderReadRepository>();
    builder.Services.AddScoped<IOrderItemReadRepository, OrderItemReadRepository>();
    builder.Services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();

    builder.Services.AddScoped<IProductRepository, ProductRepository>();
    builder.Services.AddScoped<IOrderRepository, OrderRepository>();
    builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();
    builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
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