namespace TestWebPenjualan.Domain.Helpers;

public class LoginMessageHelper
{
    public static string GetInfoLoginSuccess()
    {
        return "Successfully logged in";
    }

    public static string GetInfoLoginFailed()
    {
        return "Login failed: internal server error.";
    }

    public static string GetInfoInvalidUsernameOrPassword()
    {
        return "Invalid username or password!";
    }
}
