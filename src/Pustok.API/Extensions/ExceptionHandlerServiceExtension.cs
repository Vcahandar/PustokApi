using Microsoft.AspNetCore.Diagnostics;
using Pustok.Business.DTOs.CommonDtos;
using Pustok.Business.Exceptions;
using System.Net;

namespace Pustok.API.Extensions
{
    public static class ExceptionHandlerServiceExtension
    {
        public static IApplicationBuilder AddExceptionHandler(this IApplicationBuilder application)
        {

            application.UseExceptionHandler(error =>
            {
                error.Run(async context =>
                {
                    var feature = context.Features.Get<IExceptionHandlerFeature>();
                    HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
                    string message = "Unexpected error occured";

                    if (feature.Error is IBaseException)
                    {
                        var exception = (IBaseException)feature.Error;
                        statusCode = exception.StatusCode;
                        message = exception.ErrorMessage;
                    }


                    var response = new ResponseDto(statusCode, message);


                    context.Response.StatusCode = (int)statusCode;
                    await context.Response.WriteAsJsonAsync(response);
                    await context.Response.CompleteAsync();

                });
            });

            return application;
        }
    }
}
