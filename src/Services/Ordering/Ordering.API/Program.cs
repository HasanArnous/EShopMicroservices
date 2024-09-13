using Ordering.API;
using Ordering.Core.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplicationServices() // This layer services is before the Infrastructure because we need to use mediator in the next layer....
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices(builder.Configuration);

var app = builder.Build();

app.UseApiServices();

if (app.Environment.IsDevelopment())
    await app.InitializeDbAsync();


app.Run();
