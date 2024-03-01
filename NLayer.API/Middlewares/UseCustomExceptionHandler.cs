using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using NLayer.Core.DTOs;
using NLayer.Service.Exceptions;

namespace NLayer.API.Middlewares
{
    public static class UseCustomExceptionHandler
    {
        public static void UseCustomException(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(configure =>
                {
                    configure.Run(async context =>
                    {
                        context.Response.ContentType = "application/json";

                        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();

                        var StatusCode = exceptionHandlerPathFeature.Error switch
                        {
                            ClientSideException => 400,
                            _ => 500
                        };

                        context.Response.StatusCode = StatusCode;

                        var response = CustomeResponseDto<NoContentDto>.Fail(exceptionHandlerPathFeature.Error.Message, StatusCode);

                        await context.Response.WriteAsync(JsonSerializer.Serialize(response));

                    });
                }
            );

        }
    }
}
