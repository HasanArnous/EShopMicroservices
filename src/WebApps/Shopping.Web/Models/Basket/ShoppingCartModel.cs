namespace Shopping.Web.Models.Basket;

public class ShoppingCartModel
{
	public string Username { get; set; } = default!;
	public List<ShoppingCartItemModel> Items { get; set; } = default!;
	public decimal TotalPrice => Items == null
		? 0m
		: Items.Sum(i => i.Price * i.Quantity);
}

public class ShoppingCartItemModel
{
	public Guid ProductId { get; set; } = default!;
	public string ProductName { get; set; } = default!;
	public decimal Price { get; set; } = default!;
	public int Quantity { get; set; } = default!;
	public string Color { get; set; } = default!;
}

// Wrapper classes
public record StoreBasketRequest(ShoppingCartModel ShoppingCart);
public record StoreBasketResponse(string Username);
public record GetBasketResponse(ShoppingCartModel ShoppingCart);
public record DeleteBasketResponse(bool IsSuccess);