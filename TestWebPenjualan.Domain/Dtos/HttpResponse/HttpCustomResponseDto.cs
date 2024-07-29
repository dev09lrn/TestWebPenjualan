namespace TestWebPenjualan.Domain.Dtos.HttpResponse;

public class HttpCustomResponseDto
{
    public int StatusCode { get; set; }
    public bool Success { get; set; }
    public string? Message { get; set; }
}
