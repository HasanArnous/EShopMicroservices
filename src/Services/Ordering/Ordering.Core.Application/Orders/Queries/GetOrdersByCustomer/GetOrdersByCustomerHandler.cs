using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Ordering.Core.Application.Orders.Queries.GetOrdersByCustomer;

public class GetOrdersByCustomerHandler(IApplicationDbContext _dbContext)
    : IQueryHandler<GetOrdersByCustomerQuery, GetOrdersByCustomerResult>
{
    public async Task<GetOrdersByCustomerResult> Handle(GetOrdersByCustomerQuery query, CancellationToken cancellationToken)
    {
        var orders = await _dbContext.Orders
            .Include(o => o.OrderItems)
            .AsNoTracking()
            // The below line is replaced because of how the Customers table is configured in
            // its configuration file (CustomerConfiguration.cs) in the infrastructure layer,
            // where there is a conversation method that translate the Id from and to DB...
            //?.Where(o => o.CustomerId.Value == query.CustomerId)
			.Where(o => o.CustomerId == CustomerId.Of(query.CustomerId))
			.OrderBy(o => o.OrderName.Value)
            .ToListAsync(cancellationToken);

        return new GetOrdersByCustomerResult(orders.ToDtoList());
    }
}
