
namespace Basket.API.Basket.DeleteBasket;

public record DeleteBasketCommand(string Username):ICommand<DeleteBasketResult>;
public record DeleteBasketResult(bool IsSuccess);

public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
{
    public DeleteBasketCommandValidator()
    {
        RuleFor(x => x.Username).NotNull().NotEmpty().WithMessage("The Username cannot be null!");
    }
}

public class DeleteBasketCommandHandler(IBasketRepository repo) : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
{
    public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
    {
        ////TODO:
        /// Remove the Basket content from the Cache
        var result = await repo.DeleteBasket(command.Username, cancellationToken);
        return new DeleteBasketResult(true);
    }
}
