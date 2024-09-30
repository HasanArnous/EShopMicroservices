namespace Shopping.Web.Services;

public interface IOrderingService
{
	[Get("/ordering-service/orders/")]
	Task<GetOrdersResponse> GetOrders(int pageIndex = 0, int pageSize = 10);

	[Get("/ordering-service/orders/customer/{customerid}")]
	Task<GetOrdersByCustomerResponse> GetOrderByCustomer(Guid customerId);

	[Get("/ordering-service/orders/{orderName}")]
	Task<GetOrdersByNameResponse> GetOrderByName(string orderName);
}
