namespace TestWebPenjualan.Domain.Dtos.Product;

public class UnitType
{
    public int UnitTypeId { get; set; }
    public string Name { get; set; } = default!;
    public string? CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }
    public string? ModifiedBy { get; set; }
    public DateTime? ModifiedDate { get; set; }
}
