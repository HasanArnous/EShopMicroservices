namespace Ordering.Infrastructure.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasConversion(
            pId => pId.Value,
            fromDb => ProductId.Of(fromDb));
        builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
    }
}
