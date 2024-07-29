using TestWebPenjualan.Domain.Dtos.HttpResponse;
using TestWebPenjualan.Domain.Dtos.Product;

namespace TestWebPenjualan.Application.Interfaces;

public interface IProductApiHelper
{
    Task<List<Product>> GetProducts(GetProductsWithPagingFilter filter);
    Task<int> GetProductsRowsCount(GetProductsWithPagingFilter filter);

    Task<Product> GetProductByProductId(int productId);
    Task<List<UnitType>> GetUnitTypes();
    Task<List<Category>> GetCategories();
    Task<List<Brand>> GetBrands();
    Task<HttpCustomResponseDto> AddProduct(CreateProductDto productToAdd);
    Task<HttpCustomResponseDto> UpdateProduct(UpdateProductDto productToUpdate);
    Task<HttpCustomResponseDto> DeleteProduct(int productId);
}
