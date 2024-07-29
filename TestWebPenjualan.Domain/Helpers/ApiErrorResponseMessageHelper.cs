namespace TestWebPenjualan.Domain.Helpers;

public class ApiErrorResponseMessageHelper
{
    public static string GetInfoUnauthorizedPage()
    {
        return "Unauthorized page, please login to get the token for api authentication.";
    }

    public static string GetInfoBadRequestPage()
    {
        return "Bad request page, please use the correct format to access the api.";
    }

    public static string GetInfoForbiddenPage()
    {
        return "Forbidden access, please contact the administrator to grant the access to the api.";
    }
}
