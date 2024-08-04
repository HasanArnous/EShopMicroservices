using Discount.gRPC.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.gRPC.Data;

public class DiscountDbContext : DbContext
{
    public DiscountDbContext(DbContextOptions<DiscountDbContext> options) : base(options)
    {

    }

    public DbSet<Coupon> Coupons { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Coupon>().HasData(new Coupon[]
        {
            new Coupon 
            { 
                Id = 1,
                ProductId = new Guid("97DAAE1E-C74A-4212-AF58-B727290346B8"), 
                Amount = 150, 
                Description = "150 Discount on the NP1!"
            },
            new Coupon 
            { 
                Id = 2,
                ProductId = new Guid("E1B4B20B-D29A-45BF-998D-59A59FB83E28"), 
                Amount = 25, 
                Description = "25 Discount on the NE1!"},
        });
    }
}
