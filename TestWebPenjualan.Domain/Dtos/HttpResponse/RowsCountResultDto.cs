namespace TestWebPenjualan.Domain.Dtos.HttpResponse;

public class RowsCountResultDto
{
    public int TotalPages { get; set; }
    public int TotalRows { get; set; }
    public int LimitRowsPerPage { get; set; }
}
