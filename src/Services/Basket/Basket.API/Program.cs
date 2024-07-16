var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IBasketRepository, BasketRepository>();

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

var app = builder.Build();

app.MapCarter();

app.Run();
