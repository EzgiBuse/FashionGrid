using Newtonsoft.Json;

namespace FashionGrid.UI.Models.Dtos
{
    public class ProductListResponseDto
    {
        [JsonProperty("result")]
        public List<ProductDto> Result { get; set; }

        [JsonProperty("isSuccess")]
        public bool IsSuccess { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
