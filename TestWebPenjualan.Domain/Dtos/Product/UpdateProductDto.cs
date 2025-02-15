﻿using System.ComponentModel.DataAnnotations;

namespace TestWebPenjualan.Domain.Dtos.Product;

public class UpdateProductDto
{
    [Key]
    public int ProductId { get; set; }
    public string? ProductCode { get; set; }
    public string? Name { get; set; } 
    public int UnitTypeId { get; set; }
    public int CategoryId { get; set; }
    public int BrandId { get; set; }
    public decimal Price { get; set; }
    public string? Barcode { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }
    public string? ModifiedBy { get; set; }
    public DateTime? ModifiedDate { get; set; }
}
