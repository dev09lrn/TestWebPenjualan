namespace TestWebPenjualan.Domain.Helpers;

public class PagingHelper
{
    public static int GetRowNumberStart(int page, int rowsLimitPerPage)
    {
        return (page - 1) * rowsLimitPerPage + 1;
    }

    public static int GetRowNumberEnd(int page, int rowsLimitPerPage)
    {
        return ((page - 1) * rowsLimitPerPage) + rowsLimitPerPage;
    }
}
