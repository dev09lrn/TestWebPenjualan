using System.ComponentModel.DataAnnotations;

namespace TestWebPenjualan.Domain.Dtos.Order;

public class Order
{
    [Key]
    public long OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; } = default!;
    public int Qty { get; set; }
    public decimal TotalPrice { get; set; }
    public bool? IsDeleted { get; set; }
    public DateTime? CreatedDate { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public string? ModifiedBy { get; set; }
}
