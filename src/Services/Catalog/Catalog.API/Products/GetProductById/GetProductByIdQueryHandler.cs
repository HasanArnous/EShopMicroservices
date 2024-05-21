
namespace Catalog.API.Products.GetProductById;

public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;
public record GetProductByIdResult(Product Product);

internal class GetProductByIdQueryHandler(IDocumentSession _session, ILogger<GetProductByIdQueryHandler> _logger) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var result = await _session.LoadAsync<Product>(query.Id, cancellationToken);
        if (result == null)
            throw new ProductNotFoundException();
        return new GetProductByIdResult(result);
    }
}
