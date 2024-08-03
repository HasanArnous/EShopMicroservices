using Microsoft.EntityFrameworkCore;

namespace Discount.gRPC.Data;

public static class Extensions
{
	public static IApplicationBuilder MigrateDiscountDbContext(this IApplicationBuilder builder)
	{
		using var scope = builder.ApplicationServices.CreateScope();
		using var dbContext = scope.ServiceProvider.GetRequiredService<DiscountDbContext>();
		dbContext.Database.MigrateAsync();
		return builder;
	}
}
