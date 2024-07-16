﻿namespace Basket.API.Basket.StoreBasket;

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

public class StoreBasketCommandHandler : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        var cart = command.ShoppingCart;
        ////TODO:
        /// Store/Update the basket in the database
        /// Store/Update the basket in the cache

        return new StoreBasketResult("Hasan"); 
    }
}
