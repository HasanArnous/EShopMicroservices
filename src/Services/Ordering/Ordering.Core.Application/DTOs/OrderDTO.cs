namespace Ordering.Core.Application.DTOs;

public record OrderDTO
(
    Guid Id,
    Guid CustomerId,
    string OrderName,
    AddressDTO BillingAddress,
    AddressDTO ShippingAddress,
    PaymentDTO Payment,
    OrderStatus Status,
    List<OrderItemDTO> OrderItems
);
