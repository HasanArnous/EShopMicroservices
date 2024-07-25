namespace Basket.API.Models;

public class ShoppingCart
{
    public string Username { get; set; } = default!;
    public List<ShoppingCartItem> Items { get; set; } = default!;
    public decimal TotalPrice => Items == null 
        ? 0m 
        : Items.Sum(i => i.Price * i.Quantity);

    public ShoppingCart(string username)
    {
        Username = username;
    }

    public ShoppingCart()
    {
        
    }
}
