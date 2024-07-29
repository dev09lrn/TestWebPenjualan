using TestWebPenjualan.Application.Interfaces;

namespace TestWebPenjualan.Application.Helpers;

public class WebApiHelper : IWebApiHelper
{
    private readonly IConfiguration _configuration;
    public WebApiHelper(IConfiguration configuration)
    {
        _configuration = configuration;        
    }

    public string GetLoginUrlEndpoint()
    {
        var url = $"{_configuration["WebApi:BaseURL"]}/users/login";
        return url;
    }

    public string GetBaseProductUrlEndpoint()
    {
        var url = $"{_configuration["WebApi:BaseURL"]}/products";
        return url;
    }

    public string GetProductWithPagingRowsCountUrlEndpoint()
    {
        var url = $"{GetBaseProductUrlEndpoint()}/GetWithPagingRowsCount";
        return url;
    }

    public string GetProductWithPagingUrlEndpoint()
    {
        var url = $"{GetBaseProductUrlEndpoint()}/GetWithPaging";
        return url;
    }

    public string GetProductByProductIdUrlEndpoint(int productId)
    {
        var url = $"{GetBaseProductUrlEndpoint()}/{productId}";
        return url;
    }

    public string GetCreateProductUrlEndpoint()
    {
        var url = $"{GetBaseProductUrlEndpoint()}/AddProduct";
        return url;
    }

    public string GetUpdateProductUrlEndpoint()
    {
        var url = $"{GetBaseProductUrlEndpoint()}/UpdateProduct";
        return url;
    }

    public string GetDeleteProductUrlEndpoint()
    {
        var url = $"{GetBaseProductUrlEndpoint()}/DeleteProduct";
        return url;
    }

    public string GetUnitTypesUrlEndpoint()
    {
        var url = $"{GetBaseProductUrlEndpoint()}/GetUnitTypes";
        return url;
    }

    public string GetCategoriesUrlEndpoint()
    {
        var url = $"{GetBaseProductUrlEndpoint()}/GetCategories";
        return url;
    }

    public string GetBrandsUrlEndpoint()
    {
        var url = $"{GetBaseProductUrlEndpoint()}/GetBrands";
        return url;
    }
}
