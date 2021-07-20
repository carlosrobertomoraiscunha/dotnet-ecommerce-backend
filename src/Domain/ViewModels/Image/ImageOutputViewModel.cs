using System.Text.Json.Serialization;

namespace Domain.ViewModels.Image
{
    public class ImageOutputViewModel : ImageViewModel
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
    }
}
