﻿
namespace Catalog.API.Products.GetProductsByCategory;

public record GetProductsByCategoryQuery(string Category) : IQuery<GetProductsByCategoryResult>;
public record GetProductsByCategoryResult(IEnumerable<Product> Products);

public class GetProductsByCategoryQueryHandler
    (IDocumentSession _session, ILogger<GetProductsByCategoryQueryHandler> _logger) 
    : IQueryHandler<GetProductsByCategoryQuery, GetProductsByCategoryResult>
{
    public async Task<GetProductsByCategoryResult> Handle(GetProductsByCategoryQuery query, CancellationToken cancellationToken)
    {
        var products = await _session.Query<Product>().Where(p => p.Categories.Contains(query.Category)).ToListAsync(cancellationToken);
        return new GetProductsByCategoryResult(products);
    }
}
