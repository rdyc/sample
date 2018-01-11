using System;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Sample.Api.Exceptions;
using Sample.Api.Models.Errors;

namespace Sample.Api.Filters
{
    public class ExceptionHandlerFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var error = new {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                StatusText = "",
                Messages = context.Exception.Message,
                Trace = context.Exception.Source
            };
            
            /*var exceptionType = context.Exception.GetType();
            if (exceptionType == typeof(UnauthorizedAccessException))
            {
                message = "Unauthorized Access";
                status = HttpStatusCode.Unauthorized;
            }
            else if (exceptionType == typeof(NotImplementedException))
            {
                message = "A server error occurred.";
                status = HttpStatusCode.NotImplemented;
            }
            else if (exceptionType == typeof(ModelNotFoundException))
            {
                message = context.Exception.ToString();
                status = HttpStatusCode.InternalServerError;
            }*/
  
            HttpResponse response = context.HttpContext.Response;
            
            //context.HttpContext.Request.Headers.
            context.HttpContext.Response.StatusCode = error.StatusCode;
            context.HttpContext.Response.ContentType = "application/json";
            
            var json = JsonConvert.SerializeObject(error, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
            
            context.HttpContext.Response.WriteAsync(json);
        }
    }
}