using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestWebPenjualan.Domain.Dtos.HttpResponse;
using TestWebPenjualan.Domain.Dtos.Product;
using TestWebPenjualan.Domain.Interfaces;
using TestWebPenjualan.Infrastructure.Interfaces;

namespace TestWebPenjualan.API.Controllers
{
    [Route("api/products")]
    [Authorize]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductsController> _logger;
        private readonly IHttpCustomResponseHelper<Product> _httpCustomProductResponseHelper;
        private readonly IHttpCustomResponseHelper<UnitType> _httpCustomUnitTypeResponseHelper;
        private readonly IHttpCustomResponseHelper<Category> _httpCustomCategoryResponseHelper;
        private readonly IHttpCustomResponseHelper<Brand> _httpCustomBrandResponseHelper;
        private readonly IHttpCustomResponseHelper<RowsCountResultDto> _httpCustomRowsCountResponseHelper;
        public ProductsController(IProductService productService,
            ILogger<ProductsController> logger,
            IHttpCustomResponseHelper<Product> httpCustomProductResponseHelper,
            IHttpCustomResponseHelper<RowsCountResultDto> httpCustomRowsCountResponseHelper,
            IHttpCustomResponseHelper<UnitType> httpCustomUnitTypeResponseHelper,
            IHttpCustomResponseHelper<Category> httpCustomCategoryResponseHelper,
            IHttpCustomResponseHelper<Brand> httpCustomBrandResponseHelper
            )
        {
            _productService = productService;
            _logger = logger;
            _httpCustomProductResponseHelper = httpCustomProductResponseHelper;
            _httpCustomRowsCountResponseHelper = httpCustomRowsCountResponseHelper;
            _httpCustomUnitTypeResponseHelper = httpCustomUnitTypeResponseHelper;
            _httpCustomCategoryResponseHelper = httpCustomCategoryResponseHelper;
            _httpCustomBrandResponseHelper = httpCustomBrandResponseHelper;
        }

        [HttpGet]
        public async Task<HttpCustomResponseWithDataDto<List<Product>>> Get()
        {
            List<Product> products = new List<Product>();
            try
            {
                products = (await _productService.GetAllProducts()).ToList();

                return _httpCustomProductResponseHelper.GetHttpCustomWithDataResponse(products);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed: {ex.Message}");
                return _httpCustomProductResponseHelper.GetHttpCustomWithListDataInternalErrorResponse();
            }
        }


        [HttpGet("{productId}")]
        public async Task<HttpCustomResponseWithDataDto<Product?>> Get([FromRoute] int productId)
        {
            try
            {
                var product = await _productService.GetProductByProductId(productId);

                if (product == null)
                {
                    return _httpCustomProductResponseHelper.GetHttpCustomWithDataNotFoundErrorResponse();
                }

                return _httpCustomProductResponseHelper.GetHttpCustomWithDataResponse(product);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed: {ex.Message}");

                return _httpCustomProductResponseHelper.GetHttpCustomWithDataInternalErrorResponse();
            }
        }

        [HttpGet("GetWithPaging")]
        public async Task<HttpCustomResponseWithDataDto<List<Product>>> GetWithPaging([FromQuery] GetProductsWithPagingFilter filter)
        {
            filter.ProductCode = filter.ProductCode ?? "";
            filter.Name = filter.Name ?? "";

            List<Product> products = new List<Product>();
            try
            {
                products = (await _productService.GetProductsWithPaging(filter)).ToList();

                return _httpCustomProductResponseHelper.GetHttpCustomWithDataResponse(products);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed: {ex.Message}");
                return _httpCustomProductResponseHelper.GetHttpCustomWithListDataInternalErrorResponse();
            }
        }

        [HttpGet("GetWithPagingRowsCount")]
        public async Task<HttpCustomResponseWithDataDto<RowsCountResultDto>> GetWithPagingRowsCount([FromQuery] GetProductsWithPagingRowsCountFilter filter)
        {
            filter.ProductCode = filter.ProductCode ?? "";
            filter.Name = filter.Name ?? "";

            RowsCountResultDto result = new RowsCountResultDto();
            try
            {
                var totalRows = (await _productService.GetProductsWithPagingRowsCount(filter));
                result.TotalRows = totalRows;
                result.LimitRowsPerPage = filter.Length;
                result.TotalPages = (int) Math.Ceiling((decimal)totalRows / filter.Length);

                return _httpCustomRowsCountResponseHelper.GetHttpCustomWithValueDataResponse(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed: {ex.Message}");
                return _httpCustomRowsCountResponseHelper.GetHttpCustomWithValueDataResponse(result);
            }
        }

        [HttpPost("AddProduct")]
        public async Task<HttpCustomResponseDto> Post([FromBody] CreateProductDto productToAdd)
        {
            var currentUsername = User.Identity?.Name;
            productToAdd.CreatedBy = currentUsername;
            productToAdd.CreatedDate = DateTime.Now;

            HttpCustomResponseDto httpCustomResponse = new HttpCustomResponseDto();

            try
            {
                // Check if product code already used by other product
                GetProductsWithPagingFilter getProductsWithPagingFilter = new GetProductsWithPagingFilter
                {
                    ProductCode = productToAdd.ProductCode
                };

                var products = await _productService.GetProductsWithPaging(getProductsWithPagingFilter);

                if (products.Count() > 0)
                {
                    httpCustomResponse = _httpCustomProductResponseHelper.GetProductCodeAlreadyExistsResponse(httpCustomResponse, productToAdd.ProductCode);
                    return httpCustomResponse;
                }

                int productId = await _productService.CreateProduct(productToAdd);

                httpCustomResponse = _httpCustomProductResponseHelper.GetCreateSuccessResponse(httpCustomResponse);
                return httpCustomResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed: {ex.Message}");

                httpCustomResponse = _httpCustomProductResponseHelper.GetInternalServerErrorResponse(httpCustomResponse);
                return httpCustomResponse;
            }
        }

        [HttpPut("UpdateProduct/{productId}")]
        public async Task<HttpCustomResponseDto> Put(int productId, [FromBody] UpdateProductDto productToUpdate)
        {
            HttpCustomResponseDto httpCustomResponse = new HttpCustomResponseDto();

            var product = await _productService.GetProductByProductId(productId);

            try
            {
                if (product == null)
                {
                    httpCustomResponse = _httpCustomProductResponseHelper.GetDataNotFoundResponse(httpCustomResponse);
                    return httpCustomResponse;
                }

                if(product.ProductCode != productToUpdate.ProductCode)
                {
                    // Check if new product code already used by other product
                    GetProductsWithPagingFilter getProductsWithPagingFilter = new GetProductsWithPagingFilter
                    {
                        ProductCode = productToUpdate.ProductCode
                    };

                    var products = await _productService.GetProductsWithPaging(getProductsWithPagingFilter);

                    if (products.Count() > 0)
                    {
                        httpCustomResponse = _httpCustomProductResponseHelper.GetProductCodeAlreadyExistsResponse(httpCustomResponse, productToUpdate.ProductCode);
                        return httpCustomResponse;
                    }
                }

                var currentUsername = User.Identity?.Name;
                productToUpdate.ModifiedBy = currentUsername;
                productToUpdate.ModifiedDate = DateTime.Now;

                var updateProduct = await _productService.UpdateProduct(productId, productToUpdate);

                httpCustomResponse = _httpCustomProductResponseHelper.GetUpdateSuccessResponse(httpCustomResponse);
                return httpCustomResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed: {ex.Message}");

                httpCustomResponse = _httpCustomProductResponseHelper.GetInternalServerErrorResponse(httpCustomResponse);
                return httpCustomResponse;
            }
        }

        [HttpDelete("DeleteProduct/{productId}")]
        public async Task<HttpCustomResponseDto> Delete(int productId)
        {
            HttpCustomResponseDto httpCustomResponse = new HttpCustomResponseDto();

            try
            {
                if (await _productService.GetProductByProductId(productId) == null)
                {
                    httpCustomResponse = _httpCustomProductResponseHelper.GetDataNotFoundResponse(httpCustomResponse);
                    return httpCustomResponse;
                }

                var deleteProduct = await _productService.DeleteProduct(productId);

                httpCustomResponse = _httpCustomProductResponseHelper.GetDeleteSuccessResponse(httpCustomResponse);
                return httpCustomResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed: {ex.Message}");

                httpCustomResponse = _httpCustomProductResponseHelper.GetInternalServerErrorResponse(httpCustomResponse);
                return httpCustomResponse;
            }
        }

        [HttpGet("GetUnitTypes")]
        public async Task<HttpCustomResponseWithDataDto<List<UnitType>>> GetUnitTypes()
        {
            List<UnitType> unitTypes = new List<UnitType>();
            try
            {
                unitTypes = (await _productService.GetUnitTypes()).ToList();

                return _httpCustomUnitTypeResponseHelper.GetHttpCustomWithDataResponse(unitTypes);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed: {ex.Message}");
                return _httpCustomUnitTypeResponseHelper.GetHttpCustomWithListDataInternalErrorResponse();
            }
        }

        [HttpGet("GetCategories")]
        public async Task<HttpCustomResponseWithDataDto<List<Category>>> GetCategories()
        {
            List<Category> categories = new List<Category>();
            try
            {
                categories = (await _productService.GetCategories()).ToList();

                return _httpCustomCategoryResponseHelper.GetHttpCustomWithDataResponse(categories);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed: {ex.Message}");
                return _httpCustomCategoryResponseHelper.GetHttpCustomWithListDataInternalErrorResponse();
            }
        }

        [HttpGet("GetBrands")]
        public async Task<HttpCustomResponseWithDataDto<List<Brand>>> GetBrands()
        {
            List<Brand> brands = new List<Brand>();
            try
            {
                brands = (await _productService.GetBrands()).ToList();

                return _httpCustomBrandResponseHelper.GetHttpCustomWithDataResponse(brands);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed: {ex.Message}");
                return _httpCustomBrandResponseHelper.GetHttpCustomWithListDataInternalErrorResponse();
            }
        }
    }
}
