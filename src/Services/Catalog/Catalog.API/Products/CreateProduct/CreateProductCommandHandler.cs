using System.Reflection.Metadata;

namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand(string Name, List<string> Categories, string Description, string ImageFile, decimal Price):ICommand<CreateProductResult>;
public record CreateProductResult(Guid Id);

public class CreateProductValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductValidator()
    {
        RuleFor(p => p.Name).NotEmpty().WithMessage("The Name Field is Required");
        RuleFor(p => p.Categories).NotEmpty().WithMessage("At Least one Category Must be Added");
        RuleFor(p => p.ImageFile).NotEmpty().WithMessage("The Image File is Required");
        RuleFor(p => p.Price).GreaterThan(0).WithMessage("The Price Must be Greater than 0");
    }
}

internal class CreateProductCommandHandler(IDocumentSession _session) 
    : ICommandHandler<CreateProductCommand, CreateProductResult>
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

        _session.Store(product);
        await _session.SaveChangesAsync(cancellationToken);

        return new CreateProductResult(product.Id);
    }
}
