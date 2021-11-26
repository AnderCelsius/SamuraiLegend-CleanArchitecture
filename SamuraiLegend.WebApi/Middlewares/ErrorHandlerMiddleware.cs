using Microsoft.AspNetCore.Http;
using SamuraiLegend.Application.Exceptions;
using SamuraiLegend.Application.Wrappers;
using Serilog;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace SamuraiLegend.WebApi.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                var responseModel = Response<string>.Fail(error.Message);

                switch (error)
                {
                    case ApiException e:
                        // custom application error
                        _logger.Error(e, e.StackTrace, e.Source, e.ToString());
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        responseModel.Message = e.Message;
                        break;
                    case ValidationException e:
                        // custom application error
                        _logger.Error(e, e.StackTrace, e.Source, e.ToString());
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        responseModel.Errors = e.Errors;
                        break;
                    case KeyNotFoundException e:
                        // not found error
                        _logger.Error(e, e.StackTrace, e.Source, e.ToString());
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        responseModel.Message = e.Message;
                        break;
                    default:
                        // unhandled error
                        _logger.Error(error, error.StackTrace, error.Source, error.ToString());
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        responseModel.Message = "Internal Server Error. Please Try Again Later.";
                        break;
                }
                var result = JsonSerializer.Serialize(responseModel);

                await response.WriteAsync(result);
            }
        }
    }
}
