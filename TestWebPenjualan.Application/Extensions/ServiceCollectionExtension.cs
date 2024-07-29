using TestWebPenjualan.Application.Auth;
using TestWebPenjualan.Application.Helpers;
using TestWebPenjualan.Application.Interfaces;

namespace TestWebPenjualan.Application.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddApplicationServiceCollectionExtension(this IServiceCollection services)
    {
        services.AddScoped<AuthorizeActionFilter>();
        services.AddScoped<ILoginHelper, LoginHelper>();
        services.AddScoped<IWebApiHelper, WebApiHelper>();
        services.AddScoped<IJqueryAjaxUrlHelper, JqueryAjaxUrlHelper>();
        services.AddScoped<IProductApiHelper, ProductApiHelper>();
    }
}