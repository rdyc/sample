using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Sample.Api.Helpers;
using Sample.Api.Models.Errors;

namespace Sample.Api.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            var ex = context.Features.Get<IExceptionHandlerFeature>();
            if (ex != null)
            {
                var error = new GlobalErrorModel
                {
                    StatusCode = context.Response.StatusCode,
                    StatusText = StringHelper.Humanize(HttpStatusCode.InternalServerError.ToString()),
                    Message = ex.Error.Message,
                    //StackTrace = ex.Error.StackTrace.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None)
                    StackTrace = ex.Error.StackTrace.Split(new[] {Environment.NewLine}, StringSplitOptions.None)
                };

                var err = JsonConvert.SerializeObject(error, new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });

                await context.Response.WriteAsync(err).ConfigureAwait(false);
            }
            else
            {
                await _next(context);
            }
        }
    }
}