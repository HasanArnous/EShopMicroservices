namespace Basket.API.Data.Repositories;

public class BasketRepository(IDocumentSession session) : IBasketRepository
{
    public async Task<ShoppingCart> GetBasket(string username, CancellationToken cancellationToken = default)
    {
        var basket = await session.LoadAsync<ShoppingCart>(username, cancellationToken);
        return basket is null ? throw new BasketNotFoundException(username) : basket;
    }

    public async Task<ShoppingCart> StoreBasket(ShoppingCart cart, CancellationToken cancellationToken = default)
    {
        session.Store(cart);
        await session.SaveChangesAsync();
        return cart;
    }

    public async Task<bool> DeleteBasket(string username, CancellationToken cancellationToken = default)
    {
        session.Delete<ShoppingCart>(username);
        await session.SaveChangesAsync();
        return true;
    }
}
