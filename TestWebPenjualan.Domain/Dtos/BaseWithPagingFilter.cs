using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TestWebPenjualan.Domain.Dtos;

public class BaseWithPagingFilter
{    
    //public int RowsLimitPerPage { get; set; } = 10;
    public int Length { get; set; } = 10;
}
