using System.Text.Json.Serialization;

namespace Domain.ViewModels.Image
{
    public class ImageViewModel
    {
        [JsonPropertyName("nome")]
        public string Name { get; set; }

        [JsonPropertyName("conteudo")]
        public string Content { get; set; }
    }
}
