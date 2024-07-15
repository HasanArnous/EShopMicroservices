using BuildingBlocks.Exceptions.Handler;
using BuildingBlocks.Behaviors;
using Catalog.API.Data;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;

var builder = WebApplication.CreateBuilder(args);

#region Services Container

#region Carter Services
//? Register the services to the DI Container 
builder.Services.AddCarter();
#endregion

#region MediatR Services
//? Register all the request and request handler.
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(typeof(Program).Assembly);
    // Register all Validation Behaviors as generic type...
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    // Register the Logger Behavior
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
#endregion

#region Marten Services
//? Add and Configure Marten 
builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("CatalogDb")!);
}).UseLightweightSessions()
//.InitializeWith<CatalogInitialData>()
;
// Initialize Marten DB from the Services directly where there is a condition
if(builder.Environment.IsDevelopment())
    builder.Services.InitializeMartenWith<CatalogInitialData>();
#endregion

#region Fluent Validation Services
//? Add FluentValidation, Check the assembly for any class that use the AbstractValidator...
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
#endregion

#region HealthChecks Services
//? Add the Health Checks
builder.Services
    .AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("CatalogDb")!);
#endregion

#region Exception Handler Services
//? Register The CustomExceptionHandler
builder.Services.AddExceptionHandler<CustomExceptionHandler>();
#endregion

#endregion

var app = builder.Build();

#region Middleware Configuration

#region Exception Handler Middleware
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

#endregion

#region HealthChecks Middleware

app.UseHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
});

#endregion

#region Carter Middleware
//? Configure the HTTP pipeline/middleware
app.MapCarter();
#endregion

#endregion

app.Run();
