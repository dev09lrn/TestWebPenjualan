using Newtonsoft.Json;
using System.Net.Http.Headers;
using TestWebPenjualan.Application.Interfaces;
using TestWebPenjualan.Domain.Dtos.HttpResponse;
using TestWebPenjualan.Domain.Dtos.Product;

namespace TestWebPenjualan.Application.Helpers;

public class ProductApiHelper : IProductApiHelper
{
    private readonly ILoginHelper _loginHelper;
    private readonly IWebApiHelper _webApiHelper;
    private readonly ILogger<ProductApiHelper> _logger;
    public ProductApiHelper(ILoginHelper loginHelper,
        IWebApiHelper webApiHelper,
        ILogger<ProductApiHelper> logger)
    {
        _loginHelper = loginHelper;
        _webApiHelper = webApiHelper;
        _logger = logger;

    }
    public async Task<List<Product>> GetProducts(GetProductsWithPagingFilter filter)
    {
        List<Product> products = new List<Product>();

        try
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization
            = new AuthenticationHeaderValue("Bearer", _loginHelper.GetLoginToken());

            var url = @$"{_webApiHelper.GetProductWithPagingUrlEndpoint()}?ProductCode={filter.ProductCode}&Name={filter.Name}&UnitTypeId={filter.UnitTypeId}&Start={filter.Start}&Length={filter.Length}";

            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var responseObjInString = await response.Content.ReadAsStringAsync();

                var responseObj = JsonConvert.DeserializeObject<HttpCustomResponseWithDataDto<List<Product>>>(responseObjInString);

                if (responseObj?.Data != null)
                {
                    products = responseObj.Data;
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
        }

        return products;
    }

    public async Task<Product> GetProductByProductId(int productId)
    {
        Product product = new Product();

        try
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization
            = new AuthenticationHeaderValue("Bearer", _loginHelper.GetLoginToken());

            var url = @$"{_webApiHelper.GetProductByProductIdUrlEndpoint(productId)}";

            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var responseObjInString = await response.Content.ReadAsStringAsync();

                var responseObj = JsonConvert.DeserializeObject<HttpCustomResponseWithDataDto<Product?>>(responseObjInString);

                if (responseObj?.Data != null)
                {
                    product = responseObj.Data;
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
        }

        return product;
    }

    public async Task<int> GetProductsRowsCount(GetProductsWithPagingFilter filter)
    {
        var totalRows = 0;

        try
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization
                     = new AuthenticationHeaderValue("Bearer", _loginHelper.GetLoginToken());

            var url = @$"{_webApiHelper.GetProductWithPagingRowsCountUrlEndpoint()}?ProductCode={filter.ProductCode}&Name={filter.Name}&UnitTypeId={filter.UnitTypeId}";

            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var responseObjInString = await response.Content.ReadAsStringAsync();

                var responseObj = JsonConvert.DeserializeObject<HttpCustomResponseWithDataDto<RowsCountResultDto>>(responseObjInString);

                if (responseObj?.Data != null)
                {
                    totalRows = responseObj.Data.TotalRows;
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
        }

        return totalRows;
    }

    public async Task<HttpCustomResponseDto> AddProduct(CreateProductDto productToAdd)
    {
        var result = new HttpCustomResponseDto();
        var url = _webApiHelper.GetCreateProductUrlEndpoint();
        try
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _loginHelper.GetLoginToken());

            var response = await client.PostAsJsonAsync(url, productToAdd);
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadFromJsonAsync<HttpCustomResponseDto>();

            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
        }

        return result;
    }

    public async Task<HttpCustomResponseDto> UpdateProduct(UpdateProductDto productToUpdate)
    {
        var result = new HttpCustomResponseDto();
        var url = $"{_webApiHelper.GetUpdateProductUrlEndpoint()}/{productToUpdate.ProductId}";
        try
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _loginHelper.GetLoginToken());

            var response = await client.PutAsJsonAsync(url, productToUpdate);
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadFromJsonAsync<HttpCustomResponseDto>();

            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
        }

        return result;
    }

    public async Task<HttpCustomResponseDto> DeleteProduct(int productId)
    {
        var result = new HttpCustomResponseDto();
        var url = $"{_webApiHelper.GetDeleteProductUrlEndpoint()}/{productId}";
        try
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _loginHelper.GetLoginToken());

            var response =  await client.DeleteAsync(url);
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadFromJsonAsync<HttpCustomResponseDto>();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
        }

        return result;
    }

    public async Task<List<UnitType>> GetUnitTypes()
    {
        List<UnitType> unitTypes = new List<UnitType>();

        try
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization
                     = new AuthenticationHeaderValue("Bearer", _loginHelper.GetLoginToken());

            var url = @$"{_webApiHelper.GetUnitTypesUrlEndpoint()}";

            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var responseObjInString = await response.Content.ReadAsStringAsync();

                var responseObj = JsonConvert.DeserializeObject<HttpCustomResponseWithDataDto<List<UnitType>>>(responseObjInString);

                if (responseObj?.Data != null)
                {
                    unitTypes = responseObj.Data;
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
        }

        return unitTypes;
    }

    public async Task<List<Category>> GetCategories()
    {
        List<Category> categories = new List<Category>();

        try
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization
                     = new AuthenticationHeaderValue("Bearer", _loginHelper.GetLoginToken());

            var url = @$"{_webApiHelper.GetCategoriesUrlEndpoint()}";

            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var responseObjInString = await response.Content.ReadAsStringAsync();

                var responseObj = JsonConvert.DeserializeObject<HttpCustomResponseWithDataDto<List<Category>>>(responseObjInString);

                if (responseObj?.Data != null)
                {
                    categories = responseObj.Data;
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
        }

        return categories;
    }

    public async Task<List<Brand>> GetBrands()
    {
        List<Brand> brands = new List<Brand>();

        try
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization
                     = new AuthenticationHeaderValue("Bearer", _loginHelper.GetLoginToken());

            var url = @$"{_webApiHelper.GetBrandsUrlEndpoint()}";

            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var responseObjInString = await response.Content.ReadAsStringAsync();

                var responseObj = JsonConvert.DeserializeObject<HttpCustomResponseWithDataDto<List<Brand>>>(responseObjInString);

                if (responseObj?.Data != null)
                {
                    brands = responseObj.Data;
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
        }

        return brands;
    }
}
