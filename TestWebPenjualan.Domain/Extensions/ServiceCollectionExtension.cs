using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using TestWebPenjualan.Domain.Dtos.Register;
using TestWebPenjualan.Domain.Helpers;
using TestWebPenjualan.Domain.Interfaces;
using TestWebPenjualan.Domain.Validators;

namespace TestWebPenjualan.Domain.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddDomainServiceCollectionExtension(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();

        services.AddValidatorsFromAssemblyContaining<LoginRequestDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<RegisterRequestDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateProductDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<UpdateProductDtoValidator>();

        services.AddScoped(typeof(IHttpCustomResponseHelper<>), typeof(HttpCustomResponseHelper<>));

    }
}