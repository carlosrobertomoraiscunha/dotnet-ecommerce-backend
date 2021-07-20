using Domain.ViewModels.Image;
using System.Text.Json.Serialization;

namespace Domain.ViewModels.User
{
    public class UserOutputViewModel
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("foto")]
        public ImageOutputViewModel Photo { get; set; }

        [JsonPropertyName("nome")]
        public string Name { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("telefone")]
        public string Phone { get; set; }
    }
}
