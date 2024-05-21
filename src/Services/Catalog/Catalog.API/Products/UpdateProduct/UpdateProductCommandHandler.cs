
namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductCommand(Guid Id, string Name,List<string> Categories, string Description, string ImageFile, decimal Price) : ICommand<UpdateProductResult>;
public record UpdateProductResult(bool IsSuccess);

public class UpdateProductCommandHandler(IDocumentSession _dbSession, ILogger<UpdateProductCommandHandler> _logger) :
    ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("UpdateProductCommandHandler.Handle called with @{Command}", command);
        var product = await _dbSession.LoadAsync<Product>(command.Id);
        if (product is null)
            throw new ProductNotFoundException();

        product.Name = command.Name;
        product.Description = command.Description;
        product.ImageFile = command.ImageFile;
        product.Price = command.Price;
        product.Categories = command.Categories;
        _dbSession.Update(product);
        await _dbSession.SaveChangesAsync();
        return new UpdateProductResult(true);
    }
}
