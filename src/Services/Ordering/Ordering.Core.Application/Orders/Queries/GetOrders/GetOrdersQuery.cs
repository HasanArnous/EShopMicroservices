using BuildingBlocks.Pagination;

namespace Ordering.Core.Application.Orders.Queries.GetOrders;

public record GetOrdersQuery(PaginatedRequest PaginatedRequest) : IQuery<GetOrdersResult>;
public record GetOrdersResult(PaginatedResult<OrderDTO> PaginatedResult);
