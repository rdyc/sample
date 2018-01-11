using Newtonsoft.Json;

namespace Sample.Api.Models.Errors
{
    public class GlobalErrorModel
    {
        [JsonProperty(Order = 0)]
        public int StatusCode { get; set; }
        
        [JsonProperty(Order = 1)]
        public string StatusText { get; set; }
        
        [JsonProperty(Order = 2)]
        public string Message { get; set; }
        
        [JsonProperty(Order = 3)]
        public string[] StackTrace { get; set; }
    }
}