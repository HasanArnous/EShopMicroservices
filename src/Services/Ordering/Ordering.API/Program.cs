using Ordering.API;
using Ordering.Core.Application;
using Ordering.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddInfrastructureServices(builder.Configuration)
    .AddApplicationServices()
    .AddApiServices();

var app = builder.Build();

app.UseApiServices();

app.Run();
