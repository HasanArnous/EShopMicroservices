namespace Basket.API.Basket.DeleteBasket;

//public record DeleteBasketRequest(string Username);
public record DeleteBasketResponse(bool IsSuccess);

public class DeleteBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/Basket/{username}", async (string username, ISender sender) =>
        {
            var result = await sender.Send(new DeleteBasketCommand(username));
            return Results.Ok(result.Adapt<DeleteBasketResponse>());
        })
            .WithName("Delete Basket")
            .WithDescription("Delete the Basket from the Username")
            .WithSummary("Delete the Basket from the Username")
            .Produces(StatusCodes.Status200OK, typeof(DeleteBasketResponse))
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}
