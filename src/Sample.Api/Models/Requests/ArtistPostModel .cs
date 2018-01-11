using System.ComponentModel.DataAnnotations;

namespace Sample.Api.Models.Requests
{
    public class ArtistPostModel
    {
        [Required]
        public string name { get; set; }

        [Required]
        public string email { get; set; }
    }
}