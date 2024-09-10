namespace Ordering.Core.Application.Extensions;

public static class OrderExtensions
{
    public static IEnumerable<OrderDTO> ToDtoList(this IEnumerable<Order> orders)
    {
        return orders.Select(o => o.ToDto());
    }
    public static OrderDTO ToDto(this Order order)
    {
        return new OrderDTO(
                order.Id.Value,
                order.CustomerId.Value,
                order.OrderName.Value,
                new AddressDTO(order.BillingAddress.FirstName, order.BillingAddress.LastName, order.BillingAddress.EmailAddress, order.BillingAddress.AddressLine, order.BillingAddress.Country, order.BillingAddress.State, order.BillingAddress.ZipCode),
                new AddressDTO(order.ShippingAddress.FirstName, order.ShippingAddress.LastName, order.ShippingAddress.EmailAddress, order.ShippingAddress.AddressLine, order.ShippingAddress.Country, order.ShippingAddress.State, order.ShippingAddress.ZipCode),
                new PaymentDTO(order.Payment.CardName, order.Payment.CardNumber, order.Payment.Expiration, order.Payment.Cvv, order.Payment.PaymentMethod),
                order.Status,
                order.OrderItems.Select(oi => new OrderItemDTO(oi.Id.Value, oi.ProductId.Value, oi.Quantity, oi.Price)).ToList()
        );
    }
}
