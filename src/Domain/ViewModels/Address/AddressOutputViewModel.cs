using System.Text.Json.Serialization;

namespace Domain.ViewModels.Address
{
    public class AddressOutputViewModel : AddressViewModel
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
    }
}
