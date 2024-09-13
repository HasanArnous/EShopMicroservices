using BuildingBlocks.Exceptions.Handler;
﻿namespace Ordering.API;

namespace Ordering.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        // Register the services into the IoC container
        services.AddExceptionHandler<CustomExceptionHandler>();
        services.AddCarter();
        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app) 
    {
        // Use all the required services...
        app.UseExceptionHandler(options => { });
        app.MapCarter();
        return app;
    }
}
