namespace Ordering.Core.Application.Orders.Queries.GetOrdersByName;

public record GetOrdersByNameQuery(string OrderName) : IQuery<GetOrdersByNameResult>;
public record GetOrdersByNameResult(IEnumerable<OrderDTO> Orders);
