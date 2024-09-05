namespace Ordering.Core.Application.Orders.Commands.DeleteOrder;

public class DeleteOrderHandler(IApplicationDbContext _dbContext) : ICommandHandler<DeleteOrderCommand, DeleteOrderResult>
{
    public async Task<DeleteOrderResult> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
    {
        var order = await _dbContext.Orders.FindAsync([OrderId.Of(command.Id)], cancellationToken: cancellationToken);
        if (order == null)
            throw new OrderNotFoundException(command.Id);
        _dbContext.Orders.Remove(order);
        await _dbContext.SaveChangesAsync(cancellationToken: cancellationToken);
        return new DeleteOrderResult(true);
    }
}
