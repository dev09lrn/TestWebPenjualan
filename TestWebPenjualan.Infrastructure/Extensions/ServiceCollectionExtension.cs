using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TestWebPenjualan.Domain.Entities;
using TestWebPenjualan.Infrastructure.Interfaces;
using TestWebPenjualan.Infrastructure.Persistance.Dapper;
using TestWebPenjualan.Infrastructure.Persistance.EntityFramework;
using TestWebPenjualan.Infrastructure.Repositories;
using TestWebPenjualan.Infrastructure.Services;

namespace TestWebPenjualan.Infrastructure.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddInfrastructureServiceCollectionExtension(this IServiceCollection services)
    {
        services.AddScoped<ICustomAuthenticationService, CustomAuthenticationService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped(typeof(TestWebPenjualanDbContext));
    }

    public static void AddInfrastructureServiceAuthenticationCollectionExtension(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<UserManagementDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("UserManagementConnection")));

        services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<UserManagementDbContext>()
            .AddDefaultTokenProviders();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = configuration["JWT:ValidAudience"],
                ValidIssuer = configuration["JWT:ValidIssuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
            };
        });

    }
}