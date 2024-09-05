namespace Ordering.Core.Application.Orders.Commands.UpdateOrder;

public class UpdateOrderHandler(IApplicationDbContext _dbContext) : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
{
    public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
    {
        var orderId = OrderId.Of(command.Order.Id);
        var order = await _dbContext.Orders.FindAsync([orderId], cancellationToken: cancellationToken);
        if (order == null)
            throw new OrderNotFoundException(orderId.Value);
        UpdateOrderFromDTO(order, command.Order);
        _dbContext.Orders.Update(order);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new UpdateOrderResult(true);
    }

    private void UpdateOrderFromDTO(Order order, OrderDTO dto)
    {
        order.Update(
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
                dto.Payment.PaymentMethod),
            dto.Status);
    }
}
