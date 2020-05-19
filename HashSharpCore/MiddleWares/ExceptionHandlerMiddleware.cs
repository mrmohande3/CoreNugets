using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HashSharpCore.Filters;
using HashSharpCore.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace HashSharpCore.MiddleWares
{
    public static class ExceptionHandlerMiddlewareExtentions
    {
        public static IApplicationBuilder UseExceptionHandlerMiddleware(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate requestDelegate, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _requestDelegate = requestDelegate;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _requestDelegate(context);
            }
            catch (ApiException e)
            {
                _logger.LogError(e, "خطایی رخ داده است");
                var apiResult = new ApiResult(false, ApiResultStatusCode.ServerError, e.Message);
                context.Response.ContentType = "application/json";
                switch (e.StatusCode)
                {
                    case ApiResultStatusCode.UnAuthorized:
                        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        break;
                    case ApiResultStatusCode.BadRequest:
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    default:
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;

                }
                await context.Response.WriteAsync(JsonConvert.SerializeObject(apiResult));
            }
            catch (Exception e)
            {
                string message = string.Join('|', e.Message, e.StackTrace);
                _logger.LogError(e, "خطایی رخ داده است");
                var apiResult = new ApiResult(false, ApiResultStatusCode.ServerError, message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync(JsonConvert.SerializeObject(apiResult));
            }
        }
    }
}
