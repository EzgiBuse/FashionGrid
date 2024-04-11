using Newtonsoft.Json;

namespace FashionGrid.UI.Models.Dtos
{
    public class ProductResponseDto
    {
        [JsonProperty("result")]
        public ProductDto Result { get; set; }

        [JsonProperty("isSuccess")]
        public bool IsSuccess { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
