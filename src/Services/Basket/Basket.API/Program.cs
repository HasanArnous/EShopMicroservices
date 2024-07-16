var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("BasketDb")!);
    options.Schema.For<ShoppingCart>().Identity(sc => sc.Username);
}).UseLightweightSessions();

builder.Services.AddCarter();

var app = builder.Build();

app.MapCarter();

app.Run();
