namespace TestWebPenjualan.Domain.Helpers;

public class GeneralMessageHelper
{
    public static string GetInfoInternalServerError()
    {
        return "Failed: internal server error, please contact the administrator to check the error log.";
    }

    public static string GetInfoDataNotFound()
    {
        return "Data not found.";
    }

    public static string GetInfoProductCodeAlreadUsedByOtherProduct(string productCode)
    {
        return $"Product Code: {productCode} already used by other product.";
    }

    public static string GetInfoUpdateSuccess()
    {
        return "Data updated successfully";
    }

    public static string GetInfoCreateSuccess()
    {
        return "Data created successfully";
    }

    public static string GetInfoDeleteSuccess()
    {
        return "Data deleted successfully";
    }

    public static string GetInfoDataLoadedSuccess()
    {
        return "Data loaded successfully.";
    }

    public static string GetInfoEmptyList()
    {
        return "The list is empty.";
    }
}
