using TestWebPenjualan.Domain.Dtos.Product;
using TestWebPenjualan.Infrastructure.Interfaces;

namespace TestWebPenjualan.Infrastructure.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<Product>> GetAllProducts()
    {
        return await _productRepository.GetProducts();
    }
    public async Task<Product?> GetProductByProductId(int productId)
    {
        return await _productRepository.GetProductByProductId(productId);

    }
    public async Task<IEnumerable<Product>> GetProductsWithPaging(GetProductsWithPagingFilter filter)
    {
        return await _productRepository.GetProductsWithPaging(filter);
    }

    public async Task<int> GetProductsWithPagingRowsCount(GetProductsWithPagingRowsCountFilter filter)
    {
        return await _productRepository.GetProductsWithPagingRowsCount(filter);
    }

    public async Task<int> CreateProduct(CreateProductDto productToAdd)
    {
        return await _productRepository.CreateProduct(productToAdd);
    }

    public async Task<int> UpdateProduct(int productId, UpdateProductDto productToUpdate)
    {
        return await _productRepository.UpdateProduct(productId, productToUpdate);
    }

    public async Task<int> DeleteProduct(int productId)
    {
        return await _productRepository.DeleteProduct(productId);
    }

    public async Task<IEnumerable<UnitType>> GetUnitTypes()
    {
        return await _productRepository.GetUnitTypes();
    }

    public async Task<IEnumerable<Category>> GetCategories()
    {
        return await _productRepository.GetCategories();
    }

    public async Task<IEnumerable<Brand>> GetBrands()
    {
        return await _productRepository.GetBrands();
    }
}
