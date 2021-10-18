using System.Collections.Generic;
using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NgTemplate.API.Exceptions;
using NgTemplate.API.DTOs.Enums;

namespace NgTemplate.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (NgResponseException responseEx)
            {
                HttpStatusCode code = HttpStatusCode.InternalServerError;
                switch (responseEx.ExceptionType)
                {
                    case ExceptionType.NotFoundException:
                        code = HttpStatusCode.NotFound;
                        break;
                    case ExceptionType.InternalErrorException:
                    case ExceptionType.OperationFailedException:
                        break;
                }

                await HandleExceptionAsync(httpContext, code, responseEx);
            }
            catch (Exception ex)
            {
                var errors = new List<string> { ex.Message };
                await HandleExceptionAsync(httpContext, HttpStatusCode.InternalServerError, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, Exception ex)
        {
            _logger.LogError($"Error occurred - {ex.Message}");

            context.Response.ContentType = "application/json";

            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(JsonConvert.SerializeObject(
            new
            {
              Message = ex.Message
            }));
        }
    }
}