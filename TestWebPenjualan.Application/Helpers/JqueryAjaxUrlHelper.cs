using TestWebPenjualan.Application.Interfaces;

namespace TestWebPenjualan.Application.Helpers;

public class JqueryAjaxUrlHelper : IJqueryAjaxUrlHelper
{
    public string GetBaseUrl()
    {
        return "https://localhost:7136";
    }

    public string GetProductByPagingUrl()
    {
        return $"{GetBaseUrl()}/products/GetProductsInJson";
    }

    public string DeleteProductUrl()
    {
        return $"{GetBaseUrl()}/products/DeleteProduct";
    }
}
