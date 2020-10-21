using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }          

            catch (Exception e)
            {
                // write log
                _logger.LogError(1, e, "An error occured");

                if (e is DivideByZeroException)
                {
                    return;   
                }else
                // if is AJAX call, return JSON object anyway (we don't want full HTML page instead of JSON response)
                if (httpContext.Request.IsAjaxRequest())
                {
                    int statusCode = StatusCodes.Status500InternalServerError;
                    if (e is System.Net.Http.HttpRequestException)
                    {
                        // unfortunatelly HttpRequestException does not contain StatusCode, so parse message for extra error codes 401, 403, 404...
                        if (e.Message.Contains("401"))
                        {
                            statusCode = StatusCodes.Status401Unauthorized;
                        }
                        else if (e.Message.Contains("403"))
                        {
                            statusCode = StatusCodes.Status403Forbidden;
                        }
                        else if (e.Message.Contains("404"))
                        {
                            statusCode = StatusCodes.Status404NotFound;
                        }
                    }

                    // do not expose error messages out of API, show generic error, log file contains full stack trace
                    await PrepareResponseAsync(httpContext.Response, new Exception(e.Message), statusCode);
                    return;
                }

                // otherwise rethrow exception, will be handled by dev handler in dev and standard error code in production
                // TODO: change error page to static HTML page with CSS integrated directly inside - now frontend app with vendor.js and other files is returned - useless !
                throw;
            }
        }

        private async Task PrepareResponseAsync(HttpResponse response, Exception e, int statusCode)
        {
            response.Clear();
            response.ContentType = "application/json";
            response.StatusCode = statusCode;

            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            var json = JsonConvert.SerializeObject(new { message = e.Message }, serializerSettings);
            await response.WriteAsync(json);
        }
    }

    public static class ExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
