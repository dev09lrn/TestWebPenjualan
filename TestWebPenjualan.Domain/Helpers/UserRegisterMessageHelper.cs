using TestWebPenjualan.Domain.Entities;

namespace TestWebPenjualan.Domain.Helpers;

public class UserRegisterMessageHelper
{
    public static string GetInfoRegisterFailUserAlreadyExists()
    {
        return "User already exists.";
    }

    public static string GetInfoRegisterSuccess()
    {
        return "User has been registered successfully.";
    }
}
