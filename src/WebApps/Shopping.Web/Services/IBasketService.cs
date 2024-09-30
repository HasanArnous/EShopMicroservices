namespace Shopping.Web.Services;

public interface IBasketService
{
	[Get("/basket-service/basket/{username}")]
	Task<GetBasketResponse> GetBasket(string username);

	[Post("/basket-service/basket/{username}")]
	// Make sure the parameter name of the request body match the same name in the endpoint....
	Task<StoreBasketResponse> StoreBasket(string username, StoreBasketRequest request);

	[Delete("/basket-service/basket/{username}")]
	Task<DeleteBasketResponse> DeleteBasket(string username);

	[Post("/basket-service/basket/checkout")]
	Task<CheckoutBasketResponse> CheckoutBasket(CheckoutBasketRequest request);
}
