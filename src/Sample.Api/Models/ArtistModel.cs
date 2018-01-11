using Newtonsoft.Json;

namespace Sample.Api.Models
{
    [JsonObject(Id = "Artist")]
    public class ArtistModel : BaseModel
    {
        [JsonProperty(Order = 2)]
        public string name { get; set; }

        [JsonProperty(Order = 3)]
        public string email { get; set; }
    }
}