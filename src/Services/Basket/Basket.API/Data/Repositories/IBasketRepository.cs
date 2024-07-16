namespace Basket.API.Data.Repositories;

public interface IBasketRepository
{
    public Task<ShoppingCart> GetBasket(string username, CancellationToken cancellationToken = default);
    public Task<ShoppingCart> StoreBasket(ShoppingCart cart, CancellationToken cancellationToken = default);
    public Task<bool> DeleteBasket(string username, CancellationToken cancellationToken = default);
}
