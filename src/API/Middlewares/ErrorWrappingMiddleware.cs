using System;
using System.Net;
using System.Threading.Tasks;
using API.Models.ApiResponses;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace API.Middlewares
{
    public class ErrorWrappingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorWrappingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var response = new ApiResponse((int)HttpStatusCode.InternalServerError);
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                response = new ApiBadRequestResponse(ex.Message);
            }

            if (!context.Response.HasStarted)
            {
                context.Response.ContentType = "application/json";

                var json = JsonConvert.SerializeObject(response);

                await context.Response.WriteAsync(json);
            }
        }
    }
}