using Contacts.Core.DTOs;
using Contacts.Service.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json;

namespace ContactsAPI.Middlewares
{
    public static class UseCustomExceptionHandler
    {
        public static void UseCustomException(this IApplicationBuilder app) //Iapplication builder olma nedeni, program cs'deki var app'in web applicaation türünde olması ve web application'un da bunu miras almasından
        {
            app.UseExceptionHandler(config =>
            {
                //Run = sonlandırıcı MW
                config.Run(async context =>
                {

                    context.Response.ContentType = "application/json";

                    var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();

                    var statusCode = exceptionFeature.Error switch
                    {
                        ClientSideException => 400,
                        NotFoundException => 404,
                        _ => 500
                    };

                    context.Response.StatusCode = statusCode;


                    var response = CustomResponseDto<NoContentDto>.Fail(statusCode, exceptionFeature.Error.Message);
                    //serilaze etmemiz lazım json dönmesi için;
                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));


                });




            });
        
        }
    }
}
