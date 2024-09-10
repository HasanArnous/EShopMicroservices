namespace Ordering.Core.Application.Orders.Queries.GetOrdersByName;

public class GetOrderByNameHandler(IApplicationDbContext _dbContext) : IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
{
    public async Task<GetOrdersByNameResult> Handle(GetOrdersByNameQuery query, CancellationToken cancellationToken)
    {
        var orders = await _dbContext.Orders
            .Include(o => o.OrderItems)
            .AsNoTracking()
            .Where(o => o.OrderName.Value.Contains(query.OrderName))
            .OrderBy(o => o.OrderName.Value)
            .ToListAsync(cancellationToken);
        return new GetOrdersByNameResult(orders.ToDtoList());
    }
}
