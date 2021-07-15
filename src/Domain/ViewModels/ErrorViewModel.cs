using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Domain.ViewModels
{
    public class ErrorViewModel
    {
        [JsonPropertyName("erros")]
        public ICollection<string> Errors { get; set; }

        public ErrorViewModel()
        {
            Errors = new List<string>();
        }

        public void AddError(string error)
        {
            Errors.Add(error);
        }
    }
}
