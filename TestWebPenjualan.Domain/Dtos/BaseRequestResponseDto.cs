namespace TestWebPenjualan.Domain.Dtos;

public class BaseRequestResponseDto
{
    public int StatusCode { get; set; }
    public bool Success { get; set; }
    public string? Message { get; set; }
}
