namespace TestWebPenjualan.Domain.Dtos.HttpResponse;

public class HttpCustomResponseWithDataDto<T>
{
    public int StatusCode { get; set; }
    public bool Success { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }
}
