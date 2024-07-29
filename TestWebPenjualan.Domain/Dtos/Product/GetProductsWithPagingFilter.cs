namespace TestWebPenjualan.Domain.Dtos.Product;

public class GetProductsWithPagingFilter : BaseWithPagingFilter
{
    public string? ProductCode { get; set; }
    public string? Name { get; set; }
    public int UnitTypeId { get; set; }
    public int Page { get; set; }
    public int Draw { get; set; }
    public int Start { get; set; }

}
