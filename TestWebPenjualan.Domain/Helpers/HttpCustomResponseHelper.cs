using Microsoft.AspNetCore.Http;
using TestWebPenjualan.Domain.Dtos.HttpResponse;
using TestWebPenjualan.Domain.Interfaces;

namespace TestWebPenjualan.Domain.Helpers;

public class HttpCustomResponseHelper<T> : IHttpCustomResponseHelper<T>
{
    public HttpCustomResponseDto GetInternalServerErrorResponse(HttpCustomResponseDto httpCustomResponse)
    {
        httpCustomResponse.StatusCode = StatusCodes.Status500InternalServerError;
        httpCustomResponse.Success = false;
        httpCustomResponse.Message = GeneralMessageHelper.GetInfoInternalServerError();

        return httpCustomResponse;
    }

    public HttpCustomResponseDto GetDataNotFoundResponse(HttpCustomResponseDto httpCustomResponse)
    {
        httpCustomResponse.StatusCode = StatusCodes.Status404NotFound;
        httpCustomResponse.Success = false;
        httpCustomResponse.Message = GeneralMessageHelper.GetInfoDataNotFound();

        return httpCustomResponse;
    }

    public HttpCustomResponseDto GetProductCodeAlreadyExistsResponse(HttpCustomResponseDto httpCustomResponse, string productCode)
    {
        httpCustomResponse.StatusCode = StatusCodes.Status400BadRequest;
        httpCustomResponse.Success = false;
        httpCustomResponse.Message = GeneralMessageHelper.GetInfoProductCodeAlreadUsedByOtherProduct(productCode);

        return httpCustomResponse;
    }

    public HttpCustomResponseDto GetCreateSuccessResponse(HttpCustomResponseDto httpCustomResponse)
    {
        httpCustomResponse.StatusCode = StatusCodes.Status201Created;
        httpCustomResponse.Success = true;
        httpCustomResponse.Message = GeneralMessageHelper.GetInfoCreateSuccess();

        return httpCustomResponse;
    }

    public HttpCustomResponseDto GetUpdateSuccessResponse(HttpCustomResponseDto httpCustomResponse)
    {
        httpCustomResponse.StatusCode = StatusCodes.Status200OK;
        httpCustomResponse.Success = true;
        httpCustomResponse.Message = GeneralMessageHelper.GetInfoUpdateSuccess();

        return httpCustomResponse;
    }

    public HttpCustomResponseDto GetDeleteSuccessResponse(HttpCustomResponseDto httpCustomResponse)
    {
        httpCustomResponse.StatusCode = StatusCodes.Status200OK;
        httpCustomResponse.Success = true;
        httpCustomResponse.Message = GeneralMessageHelper.GetInfoDeleteSuccess();

        return httpCustomResponse;
    }

    public HttpCustomResponseWithDataDto<List<T>> GetHttpCustomWithDataResponse(List<T> data)
    {
        var response = new HttpCustomResponseWithDataDto<List<T>>
        {
            StatusCode = StatusCodes.Status200OK,
            Success = true,
            Message = data.Count() > 0 
                ? GeneralMessageHelper.GetInfoDataLoadedSuccess()
                : GeneralMessageHelper.GetInfoEmptyList(),
            Data = data
        };

        return response;
    }

    public HttpCustomResponseWithDataDto<List<T>> GetHttpCustomWithListDataInternalErrorResponse()
    {
        var response = new HttpCustomResponseWithDataDto<List<T>>
        {
            StatusCode = StatusCodes.Status500InternalServerError,
            Success = false,
            Message = GeneralMessageHelper.GetInfoInternalServerError(),
            Data = null
        };

        return response;
    }

    public HttpCustomResponseWithDataDto<T?> GetHttpCustomWithDataResponse(T? data)
    {
        var response = new HttpCustomResponseWithDataDto<T?>
        {
            StatusCode = StatusCodes.Status200OK,
            Success = true,
            Message = GeneralMessageHelper.GetInfoDataLoadedSuccess(),
            Data = data
        };

        return response;
    }

    public HttpCustomResponseWithDataDto<T?> GetHttpCustomWithDataInternalErrorResponse()
    {
        var response = new HttpCustomResponseWithDataDto<T?>
        {
            StatusCode = StatusCodes.Status500InternalServerError,
            Success = false,
            Message = GeneralMessageHelper.GetInfoInternalServerError()
        };

        return response;
    }

    public HttpCustomResponseWithDataDto<T?> GetHttpCustomWithDataNotFoundErrorResponse()
    {
        var response = new HttpCustomResponseWithDataDto<T?>
        {
            StatusCode = StatusCodes.Status404NotFound,
            Success = false,
            Message = GeneralMessageHelper.GetInfoDataNotFound()
        };

        return response;
    }

    public HttpCustomResponseWithDataDto<T> GetHttpCustomWithValueDataResponse(T value)
    {
        var response = new HttpCustomResponseWithDataDto<T>
        {
            StatusCode = StatusCodes.Status200OK,
            Success = true,
            Message = GeneralMessageHelper.GetInfoDataLoadedSuccess(),
            Data = value
        };

        return response;
    }
}
