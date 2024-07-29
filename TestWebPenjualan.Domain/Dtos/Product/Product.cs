using System.ComponentModel.DataAnnotations;

namespace TestWebPenjualan.Domain.Dtos.Product;

public class Product
{
    [Key]
    public int ProductId { get; set; }
    public string ProductCode { get; set; } = default!;
    public string Name { get; set; } = default!;
    public int UnitTypeId { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = default!;
    public int BrandId { get; set; }
    public string BrandName { get; set; } = default!;
    public decimal Price { get; set; }
    public string? Barcode { get; set; }
    public string? UnitTypeName { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }
    public string? ModifiedBy { get; set; }
    public DateTime? ModifiedDate { get; set; }

}
