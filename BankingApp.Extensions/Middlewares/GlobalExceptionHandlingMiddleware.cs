using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using BankingApp.Extensions.ApiResponse;
using Newtonsoft.Json;

namespace BankingApp.Extensions.Middlewares
{
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ILogger<GlobalExceptionHandlingMiddleware> logger,
            IResponseFactory responseFactory)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex, logger, context, responseFactory);
            }
        }

        private static Task HandleExceptionAsync(Exception ex, ILogger<GlobalExceptionHandlingMiddleware> logger,
            HttpContext httpContext, IResponseFactory responseFactory)
        {
            var error = (Response<object>)responseFactory.CreateErrorResponse(PredefinedErrors.General.UnhandledException);

            var supportId = error.Errors.First().SupportId;

            logger.LogError(ex, ex.Message + "{SupportId}", supportId);

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return httpContext.Response.WriteAsync(JsonConvert.SerializeObject(error));
        }


    }
}
