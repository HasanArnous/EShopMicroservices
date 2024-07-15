using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions.Handler;
using Catalog.API.Data;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Register the services to the DI Container 
builder.Services.AddCarter();

// Register all the request and request handler.
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(typeof(Program).Assembly);
    // Register all Validation Behaviors as generic type...
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    // Register the Logger Behavior
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

// Add and Configure Marten 
builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("CatalogDb")!);
}).UseLightweightSessions()
//.InitializeWith<CatalogInitialData>()
;
// Initialize Marten DB from the Services directly where there is a condition
if(builder.Environment.IsDevelopment())
    builder.Services.InitializeMartenWith<CatalogInitialData>();

// Add FluentValidation, Check the assembly for any class that use the AbstractValidator...
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

// Register The CustomExceptionHandler
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

//! Leave it empty and it will check and use the injected exception handler
app.UseExceptionHandler(options => { });

//? Replaced by the CustomExceptionHandler
// Exception Handler using the lambda...
//app.UseExceptionHandler(appBuilder =>
//{
//    appBuilder.Run(async context =>
//    {
//        var exceptionFeature = context.Features.Get<IExceptionHandlerPathFeature>();
//        var exception = exceptionFeature?.Error;
//        if(exception == null)
//        {
//            return;
//        }

//        var problem = new ProblemDetails
//        {
//            Title = exception.Message,
//            Status = StatusCodes.Status500InternalServerError,
//            Detail = exception.StackTrace
//        };

//        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
//        logger.LogError(exception, exception.Message);

//        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
//        context.Response.ContentType = "application/problem+json";
//        await context.Response.WriteAsJsonAsync(problem);
//    });
//});

// Configure the HTTP pipeline/middleware
app.MapCarter();

app.Run();
