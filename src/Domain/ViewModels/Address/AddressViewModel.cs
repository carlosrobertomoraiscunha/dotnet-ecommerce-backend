using System.Text.Json.Serialization;

namespace Domain.ViewModels.Address
{
    public class AddressViewModel
    {
        [JsonPropertyName("cep")]
        public string Cep { get; set; }

        [JsonPropertyName("bairro")]
        public string District { get; set; }

        [JsonPropertyName("rua")]
        public string Street { get; set; }

        [JsonPropertyName("cidade")]
        public string City { get; set; }

        [JsonPropertyName("estado")]
        public string State { get; set; }

        [JsonPropertyName("numero")]
        public string Number { get; set; }

        [JsonPropertyName("complemento")]
        public string Complement { get; set; }
    }
}
