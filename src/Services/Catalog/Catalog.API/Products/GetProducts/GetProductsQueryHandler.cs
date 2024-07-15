
using Marten.Pagination;

namespace Catalog.API.Products.GetProducts;

public record GetProductsQuery(int PageNumber = 1, int PageSize = 10) : IQuery<GetProductsResult>;
public record GetProductsResult(IEnumerable<Product> Products);
internal class GetProductsQueryHandler(IDocumentSession _dbSession, ILogger<GetProductsQueryHandler> _logger) : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        _logger.LogInformation("GetProductsQueryHandler.Handle called with {@query}", query);
        var products = await _dbSession.Query<Product>().ToPagedListAsync(query.PageNumber, query.PageSize, cancellationToken);
        return new GetProductsResult(products);
    }
}
