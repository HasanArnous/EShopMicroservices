namespace Ordering.Core.Application.Orders.Commands.UpdateOrder;

public record UpdateOrderCommand(OrderDTO Order) : ICommand<UpdateOrderResult>;
public record UpdateOrderResult(bool IsSuccess);

public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(x => x.Order.Id).NotNull().NotEmpty().WithMessage("Id is required!");
        RuleFor(x => x.Order.CustomerId).NotNull().NotEmpty().WithMessage("CustomerId is required!");
        RuleFor(x => x.Order.OrderName).NotEmpty().WithMessage("Name is required!");
    }
}
