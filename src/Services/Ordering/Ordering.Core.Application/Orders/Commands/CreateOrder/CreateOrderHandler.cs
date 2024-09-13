namespace Ordering.Core.Application.Orders.Commands.CreateOrder;

public class CreateOrderHandler(IApplicationDbContext _dbContext) : ICommandHandler<CreateOrderCommand, CreateOrderResult>
{
    public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var order = CreateNewOrder(command.Order);
        _dbContext.Orders.Add(order);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new CreateOrderResult(order.Id.Value);
    }

    private Order CreateNewOrder(OrderDTO dto)
    {
        var order = Order.Create(
            OrderId.Of(Guid.NewGuid()),
            CustomerId.Of(dto.CustomerId),
            OrderName.Create(dto.OrderName),
            Address.Create(
                dto.BillingAddress.FirstName,
                dto.BillingAddress.LastName,
                dto.BillingAddress.EmailAddress,
                dto.BillingAddress.AddressLine,
                dto.BillingAddress.Country,
                dto.BillingAddress.State,
                dto.BillingAddress.ZipCode), // Billing Address
            Address.Create(
                dto.ShippingAddress.FirstName,
                dto.ShippingAddress.LastName,
                dto.ShippingAddress.EmailAddress,
                dto.ShippingAddress.AddressLine,
                dto.ShippingAddress.Country,
                dto.ShippingAddress.State,
                dto.ShippingAddress.ZipCode), // Shipping Address
            Payment.Create(
                dto.Payment.CardName,
                dto.Payment.CardNumber,
                dto.Payment.Expiration,
                dto.Payment.Cvv,
                dto.Payment.PaymentMethod));

        foreach (var item in dto.OrderItems)
            order.AddItem(ProductId.Of(item.ProductId), item.Quantity, item.Price);

        return order;
    }
}
