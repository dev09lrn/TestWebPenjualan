using System.ComponentModel.DataAnnotations;

namespace TestWebPenjualan.Domain.Dtos.Product;

public class Category
{
    [Key]
    public int CategoryId { get; set; }
    public string Name { get; set; } = default!;
    public string? CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }
    public string? ModifiedBy { get; set; }
    public DateTime? ModifiedDate { get; set; }
}
