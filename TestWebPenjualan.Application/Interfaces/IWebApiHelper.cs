namespace TestWebPenjualan.Application.Interfaces;

public interface IWebApiHelper
{
    string GetLoginUrlEndpoint();
    string GetBaseProductUrlEndpoint();
    string GetProductWithPagingUrlEndpoint();
    string GetProductByProductIdUrlEndpoint(int productId);
    string GetProductWithPagingRowsCountUrlEndpoint();
    string GetCreateProductUrlEndpoint();
    string GetUpdateProductUrlEndpoint();
    string GetDeleteProductUrlEndpoint();
    string GetUnitTypesUrlEndpoint();
    string GetCategoriesUrlEndpoint();
    string GetBrandsUrlEndpoint();
}
