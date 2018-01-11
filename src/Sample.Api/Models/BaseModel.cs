using Newtonsoft.Json;

namespace Sample.Api.Models
{
    [JsonObject(Title = "Base")]
    public class BaseModel
    {
        [JsonProperty(Order = 0)]
        public long id { get; set; }
    }
}