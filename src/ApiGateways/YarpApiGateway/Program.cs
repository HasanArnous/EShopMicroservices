using Microsoft.AspNetCore.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddReverseProxy().LoadFromConfig(builder.Configuration.GetSection("ReverseProxyConfig"));
builder.Services.AddRateLimiter(rlOptions =>
{
	rlOptions.AddFixedWindowLimiter("fixed", options =>
	{
		options.Window = TimeSpan.FromSeconds(10);
		options.PermitLimit = 5;
	});
});

var app = builder.Build();

app.UseRateLimiter(); // This must be added before mapping the reverse proxy...
app.MapReverseProxy();

app.Run();
