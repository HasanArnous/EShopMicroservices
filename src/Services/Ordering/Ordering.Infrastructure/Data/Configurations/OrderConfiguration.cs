
using Ordering.Core.Domain.Enums;

namespace Ordering.Infrastructure.Data.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);
        builder.Property(o => o.Id).HasConversion(
            oId => oId.Value,
            fromDb => OrderId.Of(fromDb)
        );

        builder.HasOne<Customer>().WithMany().HasForeignKey(o => o.CustomerId).IsRequired();
        
        // Proper mapping instead of doing like this
        //! builder.HasMany<OrderItem>()
        builder.HasMany(o => o.OrderItems)
            .WithOne()
            .HasForeignKey(oi => oi.OrderId);
        builder.ComplexProperty(o => o.OrderName, nameBuilder =>
        {
            nameBuilder.Property(n => n.Value)
                       .HasColumnName(nameof(Order.OrderName))
                       .HasMaxLength(100)
                       .IsRequired();
        });
        builder.ComplexProperty(o => o.BillingAddress, billingBuilder =>
        {
            billingBuilder.Property(b => b.FirstName).HasMaxLength(50).IsRequired();
            billingBuilder.Property(b => b.LastName).HasMaxLength(50).IsRequired();
            billingBuilder.Property(b => b.AddressLine).HasMaxLength(180).IsRequired();
            billingBuilder.Property(b => b.ZipCode).HasMaxLength(5).IsRequired();
            billingBuilder.Property(b => b.State).HasMaxLength(50);
            billingBuilder.Property(b => b.Country).HasMaxLength(50);
            billingBuilder.Property(b => b.EmailAddress).HasMaxLength(50);
        });
        builder.ComplexProperty(o => o.ShippingAddress, shippingBuilder =>
        {
            shippingBuilder.Property(s => s.FirstName).HasMaxLength(50).IsRequired();
            shippingBuilder.Property(s => s.LastName).HasMaxLength(50).IsRequired();
            shippingBuilder.Property(s => s.AddressLine).HasMaxLength(180).IsRequired();
            shippingBuilder.Property(s => s.ZipCode).HasMaxLength(5).IsRequired();
            shippingBuilder.Property(s => s.State).HasMaxLength(50);
            shippingBuilder.Property(s => s.Country).HasMaxLength(50);
            shippingBuilder.Property(s => s.EmailAddress).HasMaxLength(50);
        });
        builder.ComplexProperty(o => o.Payment, paymentBuilder =>
        {
            paymentBuilder.Property(p => p.CardNumber).HasMaxLength(24).IsRequired();
            paymentBuilder.Property(p => p.Expiration).HasMaxLength(10);
            paymentBuilder.Property(p => p.CardName).HasMaxLength(50);
            paymentBuilder.Property(p => p.Cvv).HasMaxLength(3);
            // Make sure that the PaymentMethod is mapped into a column...
            paymentBuilder.Property(p => p.PaymentMethod);
        });

        builder.Property(o => o.Status)
               .HasColumnName(nameof(Order.Status))
               .HasDefaultValue(OrderStatus.Pending)
               .HasConversion(
                    s => s.ToString(),
                    fromDb => (OrderStatus)(Enum.Parse(typeof(OrderStatus), fromDb)));

        // Make sure to include this computed property, and store its value in the Database...
        builder.Property(o => o.TotalPrice);
    }
}
