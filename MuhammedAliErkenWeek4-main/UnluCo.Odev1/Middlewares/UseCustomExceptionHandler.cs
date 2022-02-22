using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using UnluCo.NetBootcamp.Odev4.Services;

namespace UnluCo.NetBootcamp.Odev4.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerService _loggerService;
        public GlobalExceptionMiddleware(RequestDelegate next, ILoggerService loggerService)
        {
            _next = next;
            _loggerService = loggerService;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var watch = Stopwatch.StartNew();
            try
            {
                string message = "[Request] Http " + context.Request.Method + " - " + context.Request.Path + " - " + DateTime.Now;
                _loggerService.Write(message);
                await _next(context);
                watch.Stop();
                message = "[Response] Http " + context.Request.Method + " - " + context.Request.Path + " - " + context.Response.StatusCode + " - response time: " + watch.Elapsed.TotalMilliseconds + "ms";
                _loggerService.Write(message);
            }
            catch (Exception ex)
            {
                watch.Stop();
                await HandleExeption(context, ex, watch);
            }
        }
        private Task HandleExeption(HttpContext context, Exception ex, Stopwatch watch)
        {
            context.Response.ContentType = "application/json"; 
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            string message = "[Error] Http " + context.Request.Method + " - " + context.Response.StatusCode +
                " - Error Message : " + ex.Message + " - response time: " + watch.Elapsed.TotalMilliseconds + "ms";
            _loggerService.Write(message);

            var result = JsonConvert.SerializeObject(new { error = "InternalServerError" }, Formatting.None);
            return context.Response.WriteAsync(result);
        }
    }
    public static class GlobalExceptionMiddlewareExtension
    {
        public static IApplicationBuilder UseGlobalExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GlobalExceptionMiddleware>();
        }
    }
}