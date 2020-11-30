using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class TransferModel<T>
    {
        [JsonPropertyName("data")]
        public T Data { get; set; }
    }
}
