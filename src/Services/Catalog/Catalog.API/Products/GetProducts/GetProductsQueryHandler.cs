
namespace Catalog.API.Products.GetProducts;

public record GetProductsQuery : IQuery<GetProductsResult>;
public record GetProductsResult(IEnumerable<Product> Products);
internal class GetProductsQueryHandler(IDocumentSession _dbSession, ILogger<GetProductsQueryHandler> _logger) : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        _logger.LogInformation("GetProductsQueryHandler.Handle called with {@query}", query);
        var products = await _dbSession.Query<Product>().ToListAsync(cancellationToken);
        return new GetProductsResult(products);
    }
}
