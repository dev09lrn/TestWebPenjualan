using System.ComponentModel.DataAnnotations;

namespace TestWebPenjualan.Domain.Dtos.Product;

public class CreateProductDto
{   
    public string? ProductCode { get; set; } = null!;
    public string? Name { get; set; } = null!;
    public int UnitTypeId { get; set; }
    public int CategoryId { get; set; }
    public int BrandId { get; set; }
    public decimal Price { get; set; }
    public string? Barcode { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }
}
