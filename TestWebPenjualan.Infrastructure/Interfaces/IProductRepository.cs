using TestWebPenjualan.Domain.Dtos.Product;

namespace TestWebPenjualan.Infrastructure.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetProducts();
    Task<IEnumerable<Product>> GetProductsWithPaging(GetProductsWithPagingFilter filter);
    Task<int> GetProductsWithPagingRowsCount(GetProductsWithPagingRowsCountFilter filter);
    Task<Product?> GetProductByProductId(int productId);
    Task<int> CreateProduct(CreateProductDto productToAdd);
    Task<int> UpdateProduct(int productId, UpdateProductDto productToUpdate);
    Task<int> DeleteProduct(int productId);
    Task<IEnumerable<UnitType>> GetUnitTypes();
    Task<IEnumerable<Category>> GetCategories();
    Task<IEnumerable<Brand>> GetBrands();
}
