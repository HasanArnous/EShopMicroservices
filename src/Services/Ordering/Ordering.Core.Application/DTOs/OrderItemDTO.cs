namespace Ordering.Core.Application.DTOs;

public record OrderItemDTO(Guid OrderId, Guid ProductId, int Quntity, decimal Price);
