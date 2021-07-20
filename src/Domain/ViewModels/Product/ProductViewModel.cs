using Domain.ViewModels.Image;
using System.Text.Json.Serialization;

namespace Domain.ViewModels.Product
{
    public class ProductViewModel
    {
        [JsonPropertyName("imagem")]
        public ImageViewModel Picture { get; set; }

        [JsonPropertyName("nome")]
        public string Name { get; set; }

        [JsonPropertyName("preco")]
        public decimal Price { get; set; }

        [JsonPropertyName("descricao")]
        public string Description { get; set; }

        [JsonPropertyName("categoria")]
        public string Category { get; set; }
    }
}
