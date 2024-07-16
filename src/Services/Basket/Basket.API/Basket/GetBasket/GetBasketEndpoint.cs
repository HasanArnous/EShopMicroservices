namespace Basket.API.Basket.GetBasket;

// public record GetBasketRequest(string Username);
public record GetBasketResponse(ShoppingCart ShoppingCart);
public class GetBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/Basket/{username}", async (string username, ISender sender) =>
        {
            var getBasketQuery = new GetBasketQuery(username);
            var result = await sender.Send(getBasketQuery);
            return Results.Ok(result.Adapt<GetBasketResponse>());
        })
            .WithName("GetBasket")
            .WithDescription("Get Basket from the Username")
            .WithSummary("Get the Basket from the Username")
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}
