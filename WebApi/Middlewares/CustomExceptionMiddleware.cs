using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using WebApi.Services;

namespace WebApi.Middlewares
{
    public class CustomExceptionMiddleware
    {
        // Next middleware object
        private readonly RequestDelegate _next;

        // Logger service
        private readonly ILoggerService _logger;

        public CustomExceptionMiddleware(RequestDelegate next, ILoggerService logger)
        {
            _next = next;
            _logger = logger;
        }

        // Special method for middlewares and it gets called by previous middleware
        public async Task Invoke(HttpContext httpContext)
        {
            // Used for calculate passed time between a request and response to this request
            // Starts a timer
            var watch = Stopwatch.StartNew();

            try
            {
                string message = "[Request]  HTTP " + httpContext.Request.Method + " - " + httpContext.Request.Path;
                _logger.Write(message);

                await _next(httpContext); // same as _next.Invoke(httpContext)

                watch.Stop(); // Stops the timer

                message = "[Response] HTTP " + httpContext.Request.Method + " - " + httpContext.Request.Path
                          + " responded " + httpContext.Response.StatusCode + " in " + watch.ElapsedMilliseconds + " ms.";
                _logger.Write(message);
            }
            catch (Exception e)
            {
                // If the next middleware (in this case this middleware is endpoints)
                // throws an error (e.g. in the service layer of an endpoint)
                // it will be caught in this catch block 
                // (check the middleware methods from documentation for more information)

                watch.Stop();
                await HandleException(httpContext, watch, e);
            }
        }

        public Task HandleException(HttpContext httpContext, Stopwatch watch, Exception e)
        {
            // In the exception case the response should be an error message 
            httpContext.Response.ContentType = "application/json";
            
            // Sets the response status code as 500 Internal Server Error
            httpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

            string message = "[Error]    HTTP " + httpContext.Request.Method + " - " + httpContext.Response.StatusCode
                             + " Error message " + e.Message + " in " + watch.ElapsedMilliseconds + " ms.";
            _logger.Write(message);

            // Converts error message to serialized json object 
            var result = JsonConvert.SerializeObject(new {error = e.Message}, Formatting.None);

            // Writes serialized error message to the response
            return httpContext.Response.WriteAsync(result);
        }
    }
    
    public static class CustomExceptionMiddlewareExtension
    {
        // This extension method adds CustomExceptionMiddleware to the middleware pipeline
        public static IApplicationBuilder UseCustomException(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}