using BuildingBlocks.Behaviors;
using Microsoft.Extensions.DependencyInjection;
using BuildingBlocks.Messaging.MassTransit;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace Ordering.Core.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Register the services into the IoC container...
        services.AddMediatR(config => {
            config.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            config.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });
        services.AddMessageBroker(configuration, Assembly.GetExecutingAssembly());


		return services;
    }
}
