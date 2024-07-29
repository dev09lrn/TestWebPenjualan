namespace TestWebPenjualan.Domain.Dtos.Product;

public class GetProductsWithPagingRowsCountFilter : BaseWithPagingFilter
{
    public string? ProductCode { get; set; } 
    public string? Name { get; set; } 
    public int UnitTypeId { get; set; }

}
