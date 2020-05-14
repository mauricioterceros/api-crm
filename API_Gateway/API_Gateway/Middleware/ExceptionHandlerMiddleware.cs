using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using BackingServices.Exceptions;
using Newtonsoft.Json;

namespace API_Gateway.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleError(httpContext, ex);
            }
        }
        private static Task HandleError(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            var errorObj = new
            {
                code = getCode(ex),
                message = ex.Message
            };

            string jsonObj = JsonConvert.SerializeObject(errorObj);
            context.Response.StatusCode = getCode(ex);
            return context.Response.WriteAsync(jsonObj);
        }
        private static int getCode(Exception ex)
        {

            int code = 500;
            //Agarra el tipo de exception 
            if (ex.GetType() == typeof(BackingServiceException))
                code = ((BackingServiceException)ex).Code;
            if (ex.GetType() == typeof(BadRequestException))
                code = ((BadRequestException)ex).Code;


            return code;
        }

    }

    public static class ExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandlerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
