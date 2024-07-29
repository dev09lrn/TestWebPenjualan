using TestWebPenjualan.Domain.Dtos.HttpResponse;

namespace TestWebPenjualan.Domain.Interfaces;

public interface IHttpCustomResponseHelper<T>
{
    HttpCustomResponseDto GetCreateSuccessResponse(HttpCustomResponseDto httpCustomResponse);
    HttpCustomResponseDto GetDataNotFoundResponse(HttpCustomResponseDto httpCustomResponse);
    HttpCustomResponseDto GetDeleteSuccessResponse(HttpCustomResponseDto httpCustomResponse);
    HttpCustomResponseWithDataDto<List<T>> GetHttpCustomWithDataResponse(List<T> data);
    HttpCustomResponseWithDataDto<List<T>> GetHttpCustomWithListDataInternalErrorResponse();
    HttpCustomResponseWithDataDto<T?> GetHttpCustomWithDataResponse(T? data);
    HttpCustomResponseWithDataDto<T> GetHttpCustomWithValueDataResponse(T value);
    HttpCustomResponseWithDataDto<T?> GetHttpCustomWithDataInternalErrorResponse();
    HttpCustomResponseWithDataDto<T?> GetHttpCustomWithDataNotFoundErrorResponse();
    HttpCustomResponseDto GetInternalServerErrorResponse(HttpCustomResponseDto httpCustomResponse);
    HttpCustomResponseDto GetUpdateSuccessResponse(HttpCustomResponseDto httpCustomResponse);
    HttpCustomResponseDto GetProductCodeAlreadyExistsResponse(HttpCustomResponseDto httpCustomResponse, string productCode);
}
