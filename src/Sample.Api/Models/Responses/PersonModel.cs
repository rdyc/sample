using Newtonsoft.Json;

namespace Sample.Api.Models.Responses
{
    [JsonObject(Id = "Person")]
    public class PersonModel : BaseModel
    {
        [JsonProperty(Order = 2)]
        public string Name { get; set; }
        
        [JsonProperty(Order = 3)]
        public string Email { get; set; }
    }
}