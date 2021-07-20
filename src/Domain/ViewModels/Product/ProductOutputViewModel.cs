using Domain.ViewModels.Image;
using System.Text.Json.Serialization;

namespace Domain.ViewModels.Product
{
    public class ProductOutputViewModel
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("imagem")]
        public ImageOutputViewModel Picture { get; set; }

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
