namespace Shopping.Web.Services;

public interface ICatalogService
{
	[Get("/catalog-service/products?pageNumber={pageNumber}&pageSize={pageSize}")]
	Task<GetProductsResponse> GetProducts(int? pageNumber = 1, int? pageSize = 10);

	[Get("/catalog-service/products/category/{categoryId}")]
	Task<GetProductsByCategoryResponse> GetProductsByCategory(string categoryId);

	[Get("/catalog-service/products/{productId}")]
	Task<GetProductByIdResponse> GetProductById(Guid productId);
}
