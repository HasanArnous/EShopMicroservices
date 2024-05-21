var builder = WebApplication.CreateBuilder(args);

// Register the services to the DI Container 
builder.Services.AddCarter();

// Register all the request and request handler.
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(typeof(Program).Assembly);
});

// Add and Configure Marten 
builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("CatalogDb")!);
}).UseLightweightSessions();

var app = builder.Build();

// Configure the HTTP pipeline/middleware
app.MapCarter();

app.Run();
