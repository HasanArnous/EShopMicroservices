namespace Ordering.Core.Application.Exceptions;

public class OrderNotFoundException : NotFoundException
{
    public OrderNotFoundException(Guid id) : base("Order", id)
    {
    }
}
