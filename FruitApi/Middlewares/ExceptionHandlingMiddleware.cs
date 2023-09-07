using FruitApi.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;
using Newtonsoft.Json;

namespace FruitApi.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ArgumentException ex)
            {
                await HandleExceptionAsync(context, ex, 400, "BadRequest");
            }
            catch (NotFoundException ex)
            {
                await HandleExceptionAsync(context, ex, 404, "NotFound");
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, 500, "InternalServerError");
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception, int statusCode, string statusText)
        {
            var result = JsonConvert.SerializeObject(new
            {
                Status = statusCode,
                Message = exception.Message,
                Date = DateTime.Now
            });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            return context.Response.WriteAsync(result);
        }
    }

    public static class ExceptionHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
