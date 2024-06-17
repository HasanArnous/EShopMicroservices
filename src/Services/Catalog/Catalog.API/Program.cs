using BuildingBlocks.Behaviors;

var builder = WebApplication.CreateBuilder(args);

// Register the services to the DI Container 
builder.Services.AddCarter();

// Register all the request and request handler.
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(typeof(Program).Assembly);
    // Register all Validation Behaviors as generic type...
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

// Add and Configure Marten 
builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("CatalogDb")!);
}).UseLightweightSessions();

// Add FluentValidation, Check the assembly for any class that use the AbstractValidator...
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

var app = builder.Build();

// Configure the HTTP pipeline/middleware
app.MapCarter();

app.Run();
