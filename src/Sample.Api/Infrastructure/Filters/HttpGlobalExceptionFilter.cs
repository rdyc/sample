using System;

namespace Sample.Api.Infrastructure.Filters
{
    using Microsoft.AspNetCore.Mvc;
    using global::Sample.Api.Exceptions;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Sample.Api.Infrastructure.ActionResults;
    using Microsoft.Extensions.Logging;
    using System.Net;

    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private readonly IHostingEnvironment env;
        private readonly ILogger<HttpGlobalExceptionFilter> logger;

        public HttpGlobalExceptionFilter(IHostingEnvironment env, ILogger<HttpGlobalExceptionFilter> logger)
        {
            this.env = env;
            this.logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            logger.LogError(new EventId(context.Exception.HResult),
                context.Exception,
                context.Exception.Message);

            if (context.Exception.GetType() == typeof(SampleDomainException))
            {
                var json = new JsonErrorResponse
                {
                    Messages = context.Exception.Message
                };

                // Result asigned to a result object but in destiny the response is empty. This is a known bug of .net core 1.1
                //It will be fixed in .net core 1.1.2. See https://github.com/aspnet/Mvc/issues/5594 for more information
                context.Result = new BadRequestObjectResult(json);
                context.HttpContext.Response.StatusCode = (int) HttpStatusCode.BadRequest;
            }
            else
            {
                var json = new JsonErrorResponse
                {
                    Messages = "An error occured!"
                };

                if (env.IsDevelopment())
                {
                    //json.DeveloperMessage = context.Exception;
                    json.StackTrace = new
                    {
                        Message = context.Exception.Message,
                        Trace = context.Exception.StackTrace.Split(new[] {Environment.NewLine}, StringSplitOptions.None)
                    };
                }
            

                // Result asigned to a result object but in destiny the response is empty. This is a known bug of .net core 1.1
                // It will be fixed in .net core 1.1.2. See https://github.com/aspnet/Mvc/issues/5594 for more information
                context.Result = new InternalServerErrorObjectResult(json);
                context.HttpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            }
            context.ExceptionHandled = true;
        }

        private class JsonErrorResponse
        {
            public string Messages { get; set; }

            public object StackTrace { get; set; }
        }
    }
}