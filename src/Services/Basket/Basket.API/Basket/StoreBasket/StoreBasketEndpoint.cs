namespace Basket.API.Basket.StoreBasket;

public record StoreBasketRequest(ShoppingCart ShoppingCart);
public record StoreBasketResponse(string Username);

public class StoreBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/Basket/{username}", async (StoreBasketRequest request, ISender sender) =>
        {
            var commandResult = await sender.Send(request.Adapt<StoreBasketCommand>());
            var response = commandResult.Adapt<StoreBasketResponse>();
            return Results.Created($"/Basket/{response.Username}", response);
        })
            .WithName("StoreBasket")
            .WithDescription("Store basket shopping cart")
            .WithSummary("Store basket shopping cart")
            .Produces(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}
