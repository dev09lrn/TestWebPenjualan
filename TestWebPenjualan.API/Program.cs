using Microsoft.OpenApi.Models;
using System.Net;
using TestWebPenjualan.Domain.Dtos.HttpResponse;
using TestWebPenjualan.Domain.Extensions;
using TestWebPenjualan.Domain.Helpers;
using TestWebPenjualan.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;


builder.Services.AddControllers();

builder.Services.AddDomainServiceCollectionExtension();

builder.Services.AddInfrastructureServiceAuthenticationCollectionExtension(builder.Configuration);
builder.Services.AddInfrastructureServiceCollectionExtension();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Test Web Penjualan API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowClientWebApp",
        b =>
        {
            b
                .WithOrigins("https://localhost:7136")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowClientWebApp");

app.UseHttpsRedirection();

// Custom error response
app.UseStatusCodePages(async statusCodeContext =>
{
    switch (statusCodeContext.HttpContext.Response.StatusCode)
    {
        case StatusCodes.Status401Unauthorized:
            statusCodeContext.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await statusCodeContext.HttpContext.Response.WriteAsJsonAsync(new HttpCustomResponseDto { 
                StatusCode = StatusCodes.Status401Unauthorized, 
                Success = false,
                Message = ApiErrorResponseMessageHelper.GetInfoUnauthorizedPage()
            });
            break;
        case StatusCodes.Status403Forbidden:
            statusCodeContext.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
            await statusCodeContext.HttpContext.Response.WriteAsJsonAsync(new HttpCustomResponseDto {
                StatusCode = StatusCodes.Status403Forbidden,
                Success = false,
                Message = ApiErrorResponseMessageHelper.GetInfoForbiddenPage()
            });
            break;
        case StatusCodes.Status400BadRequest:
            statusCodeContext.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            await statusCodeContext.HttpContext.Response.WriteAsJsonAsync(new HttpCustomResponseDto
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Success = false,
                Message = ApiErrorResponseMessageHelper.GetInfoBadRequestPage()
            });
            break;
    }
});


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
