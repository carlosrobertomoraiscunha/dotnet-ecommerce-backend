using Domain.ViewModels.Image;
using System.Text.Json.Serialization;

namespace Domain.ViewModels.User
{
    public class UserUpdateViewModel
    {
        [JsonPropertyName("foto")]
        public ImageViewModel Photo { get; set; }

        [JsonPropertyName("nome")]
        public string Name { get; set; }

        [JsonPropertyName("senha")]
        public string Password { get; set; }

        [JsonPropertyName("telefone")]
        public string Phone { get; set; }
    }
}
