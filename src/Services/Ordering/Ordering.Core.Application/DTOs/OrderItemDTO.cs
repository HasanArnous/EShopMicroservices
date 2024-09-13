namespace Ordering.Core.Application.DTOs;

public record OrderItemDTO(Guid OrderId, Guid ProductId, int Quantity, decimal Price);
