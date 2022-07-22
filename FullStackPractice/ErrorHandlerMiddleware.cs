using FullStackPractice.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace FullStackPractice.Web
{
    public class ErrorHandlerMiddleware
    {

        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
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

                switch (error)
                {
                    case ServiceException e:
                        // custom app error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case KeyNotFoundException e:
                        // not found error
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        // unhandled
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;

                }


                var result = JsonSerializer.Serialize(
                    new { statusCode = response.StatusCode, message = error?.Message }
                );

                await response.WriteAsync(result);
            }
        }
    }
}
