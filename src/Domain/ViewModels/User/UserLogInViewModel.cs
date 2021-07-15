using System.Text.Json.Serialization;

namespace Domain.ViewModels.User
{
    public class UserLogInViewModel
    {
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("senha")]
        public string Password { get; set; }
    }
}
