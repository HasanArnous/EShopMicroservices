var builder = WebApplication.CreateBuilder(args);

// Register the services to the DI Container 

var app = builder.Build();

// Configure the HTTP pipeline/middleware

app.Run();
