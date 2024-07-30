using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace Basket.API.Data.Repositories;

public class CachedBasketRepository(IBasketRepository repo, IDistributedCache cache) : IBasketRepository
{
    public async Task<ShoppingCart> GetBasket(string username, CancellationToken cancellationToken = default)
    {
        var cachedBasket = await cache.GetStringAsync(username, cancellationToken);
        if(!string.IsNullOrEmpty(cachedBasket))
            return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket)!;
        var basket = await repo.GetBasket(username, cancellationToken);
        await cache.SetStringAsync(username, JsonSerializer.Serialize(basket), cancellationToken);
        return basket;
    }

    public async Task<ShoppingCart> StoreBasket(ShoppingCart cart, CancellationToken cancellationToken = default)
    {
        var basket = await repo.StoreBasket(cart, cancellationToken);
        await cache.SetStringAsync(cart.Username, JsonSerializer.Serialize(basket), cancellationToken);
        return basket;
    }
    public async Task<bool> DeleteBasket(string username, CancellationToken cancellationToken = default)
    {
        var result = await repo.DeleteBasket(username, cancellationToken);
        if(result) await cache.RemoveAsync(username, cancellationToken);
        return result;
    }
}
