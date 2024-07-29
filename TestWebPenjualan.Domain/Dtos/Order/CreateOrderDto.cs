namespace TestWebPenjualan.Domain.Dtos.Order;

public class CreateOrderDto
{
    public DateTime OrderDate { get; set; }
    public int ProductId { get; set; } 
    public int Qty { get; set; }
    public decimal TotalPrice { get; set; }
    public bool? IsDeleted { get; set; }
    public DateTime? CreatedDate { get; set; }
    public string? CreatedBy { get; set; }
}
