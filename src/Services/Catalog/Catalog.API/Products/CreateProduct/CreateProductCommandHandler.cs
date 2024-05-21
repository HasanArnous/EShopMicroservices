
namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand(string Name, List<string> Categories, string Description, string ImageFile, decimal Price):ICommand<CreateProductResult>;
public record CreateProductResult(Guid Id);

internal class CreateProductCommandHandler(IDocumentSession _session) : ICommandHandler<CreateProductCommand, CreateProductResult>
{

    async Task<CreateProductResult> IRequestHandler<CreateProductCommand, CreateProductResult>.Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = command.Name,
            Categories = command.Categories,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Price = command.Price
        };

        // TODO:
        // Save to database
        _session.Store(product);
        await _session.SaveChangesAsync(cancellationToken);

        return new CreateProductResult(product.Id);
    }
}
