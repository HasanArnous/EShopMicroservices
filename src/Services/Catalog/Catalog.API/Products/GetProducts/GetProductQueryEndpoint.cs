
namespace Catalog.API.Products.GetProducts;

public record GetProductQueryResponse(IEnumerable<Product> Products);

public class GetProductQueryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (ISender sender) =>
        {
            var result = await sender.Send(new GetProductsQuery());
            var response = result.Adapt<GetProductQueryResponse>();
            return Results.Ok(response);
        }).WithName("GetProducts")
        .Produces<GetProductQueryResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithDescription("Get all Products")
        .WithSummary("Get all Products");
    }
}
