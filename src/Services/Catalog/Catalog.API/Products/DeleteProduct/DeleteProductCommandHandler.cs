
namespace Catalog.API.Products.DeleteProduct;

public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;
public record DeleteProductResult(bool IsSuccess);

public class DeleteCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteCommandValidator()
    {
        RuleFor(p => p.Id).NotEmpty().WithMessage("The Id is Required");
    }
}

internal class DeleteProductCommandHandler(IDocumentSession _session) 
    : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        var product = await _session.LoadAsync<Product>(command.Id, cancellationToken);
        if (product is null)
            throw new ProductNotFoundException(command.Id);
        _session.Delete(product);
        await _session.SaveChangesAsync();
        return new DeleteProductResult(true);
    }
}
