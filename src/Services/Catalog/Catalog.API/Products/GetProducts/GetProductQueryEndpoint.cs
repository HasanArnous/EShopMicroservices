
namespace Catalog.API.Products.GetProducts;

public record GetProductQueryRequest(int PageNumber = 1, int PageSize = 10);
public record GetProductQueryResponse(IEnumerable<Product> Products);

public class GetProductQueryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async ([AsParameters] GetProductQueryRequest request, ISender sender) =>
        {
            var query = request.Adapt<GetProductsQuery>();
            var result = await sender.Send(query);
            var response = result.Adapt<GetProductQueryResponse>();
            return Results.Ok(response);
        }).WithName("GetProducts")
        .Produces<GetProductQueryResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithDescription("Get all Products")
        .WithSummary("Get all Products");
    }
}
