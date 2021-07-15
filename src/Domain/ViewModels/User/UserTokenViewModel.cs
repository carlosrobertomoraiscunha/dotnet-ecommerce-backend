using System.Text.Json.Serialization;

namespace Domain.ViewModels.User
{
    public class UserTokenViewModel : UserOutputViewModel
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }
    }
}
