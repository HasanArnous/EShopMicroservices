namespace Ordering.Core.Application.Orders.Queries.GetOrders;

public class GetOrdersHandler(IApplicationDbContext _dbContext)
    : IQueryHandler<GetOrdersQuery, GetOrdersResult>
{
    public async Task<GetOrdersResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
    {
        var index = query.PaginatedRequest.PageIndex;
        var size = query.PaginatedRequest.PageSize;

        var count = await _dbContext.Orders.LongCountAsync(cancellationToken);

        var orders = await _dbContext.Orders
            .Include(o => o.OrderItems)
            .OrderBy(o => o.OrderName.Value)
            .AsNoTracking()
            .Skip(index * size)
            .Take(size)
            .ToListAsync(cancellationToken);

        var result = new PaginatedResult<OrderDTO>(index, size, count, orders.ToDtoList());
        return new GetOrdersResult(result);
    }
}
