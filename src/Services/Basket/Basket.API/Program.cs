using BuildingBlocks.Exceptions.Handler;
using HealthChecks.UI.Client;
using Microsoft.Extensions.Caching.Distributed;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
//? Registering the decoration with Scrutor
builder.Services.Decorate<IBasketRepository, CachedBasketRepository>();
//? Registering the decoration manually
//builder.Services.AddScoped<IBasketRepository>(provider =>
//{
//    var basketRepo = provider.GetRequiredService<IBasketRepository>();
//    return new CachedBasketRepository(basketRepo, provider.GetRequiredService<IDistributedCache>());
//});

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddMarten(options =>
{
    string cs = builder.Configuration.GetConnectionString("BasketDb")!;
    options.Connection(cs);
    options.Schema.For<ShoppingCart>().Identity(sc => sc.Username);
}).UseLightweightSessions();

builder.Services.AddCarter();

builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("BasketDb")!)
    .AddRedis(builder.Configuration.GetConnectionString("Redis")!);

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

app.UseExceptionHandler(options => { });

app.MapHealthChecks("/Health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions {
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.MapCarter();

app.Run();
