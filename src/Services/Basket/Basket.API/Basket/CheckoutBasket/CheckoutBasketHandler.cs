using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Basket.API.Basket.CheckoutBasket;

public record CheckoutBasketCommand(BasketCheckoutDTO BasketCheckout) : ICommand<CheckoutBasketResult>;
public record CheckoutBasketResult(bool IsSuccess);

public class CheckoutBasketCommandValidator : AbstractValidator<CheckoutBasketCommand>
{
    
    public CheckoutBasketCommandValidator()
    {
        RuleFor(x => x.BasketCheckout).NotNull().WithMessage("CheckoutBasketCommand cannot be null!");
        RuleFor(x => x.BasketCheckout.UserName).NotNull().NotEmpty().WithMessage("Username cannot be empty or null!");
    }
}

public class CheckoutBasketHandler(IBasketRepository _basketRepo, IPublishEndpoint _publisher)
    : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
{
    public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
    {
        var basket = await _basketRepo.GetBasket(command.BasketCheckout.UserName, cancellationToken);
        if (basket == null)
            return new CheckoutBasketResult(false);
        var eventMess = command.BasketCheckout.Adapt<BasketCheckoutEvent>();
        eventMess.TotalPrice = basket.TotalPrice;
        await _publisher.Publish(eventMess, cancellationToken);
        await _basketRepo.DeleteBasket(command.BasketCheckout.UserName, cancellationToken);
        return new CheckoutBasketResult(true);
    }
}
