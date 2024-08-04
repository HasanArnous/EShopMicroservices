using Discount.gRPC;

namespace Basket.API.Basket.StoreBasket;

public record StoreBasketCommand(ShoppingCart ShoppingCart) : ICommand<StoreBasketResult>;
public record StoreBasketResult(string Username);

public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(x => x).NotNull().WithMessage("The Cart cannot be null!");
        RuleFor(x => x.ShoppingCart.Username).NotNull().NotEmpty().WithMessage("The Username cannot be empty!");
    }
}

public class StoreBasketCommandHandler
    (IBasketRepository repo, DiscountProtoService.DiscountProtoServiceClient discountClient) 
    : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        var cart = command.ShoppingCart;
        await DeductDiscount(cart, cancellationToken);
        var basket = await repo.StoreBasket(command.ShoppingCart);
        return new StoreBasketResult(basket.Username); 
    }

    private async Task DeductDiscount(ShoppingCart cart, CancellationToken ct)
    {
        foreach(var item in cart.Items)
        {
            var discount = await discountClient.GetDiscountAsync(new GetDiscountRequest { ProductId = item.ProductId.ToString() }, cancellationToken:ct);
            item.Price -= (decimal)(discount.Amount);
        }
    }
}
