﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using UnluCoWeek5.Services;

namespace UnluCoWeek5.Middleware
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerService _loggerService;

        public CustomExceptionMiddleware(RequestDelegate next, ILoggerService loggerService)
        {
            _next = next;
            _loggerService = loggerService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            {
                var watch = Stopwatch.StartNew();
                try
                {
                    string message = "[Request]  HTTP" + context.Request.Method + "-" + context.Request.Path;
                    await _next(context);
                    watch.Stop(); 

                    message = "[Response]  HTTP" + context.Request.Method + "-" + context.Request.Path + "responded" + context.Response.StatusCode + "in" + watch.Elapsed.TotalMilliseconds + "ms";
                    _loggerService.Write(message);
                }
                catch (Exception ex)
                {
                    watch.Stop();
                    await HandleException(context, ex, watch);
                }

            }
        }

        private Task HandleException(HttpContext context, Exception ex, Stopwatch watch)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            string message = "[Error]  HTTP " + context.Request.Method + "-" + context.Response.StatusCode + "Error Message: " + ex.Message;
            _loggerService.Write(message);
            var result = JsonConvert.SerializeObject(new { error = ex.Message }, Formatting.None);
            return context.Response.WriteAsync(result);
        }
    }
    public static class CustomExceptionMiddlewareExtension
    {
        public static IApplicationBuilder UseCustomMiddleware(this IApplicationBuilder applicationBuilder)
        {
            
            return applicationBuilder.UseMiddleware<CustomExceptionMiddleware>();
        }
    }


}
