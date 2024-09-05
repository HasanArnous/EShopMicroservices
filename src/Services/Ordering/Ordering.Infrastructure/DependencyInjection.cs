using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Core.Application.Data;

namespace Ordering.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices
        (this IServiceCollection services, IConfiguration configuration)
    {
        // Register the Services into the IoC container...
        var connectionString = configuration.GetConnectionString("OrderingDb");

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        // Registering the DbContext
        services.AddDbContext<ApplicationDbContext>((sp, options )=>
        {

            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseSqlServer(connectionString);
        });

        // Registering the Abstract interface for our Db Context
        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

        return services;
    }
}
