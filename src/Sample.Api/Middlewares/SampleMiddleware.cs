using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Sample.Api.Middlewares
{
    public class SampleMiddleware
    {
        private readonly RequestDelegate _next;

        public SampleMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            /*context.Response.ContentType = "application/json";
            var json = JsonConvert.SerializeObject(new { message = CultureInfo.CurrentCulture.FirstName });
            
            await context.Response.WriteAsync(json);*/

            await _next(context);
        }
    }
}