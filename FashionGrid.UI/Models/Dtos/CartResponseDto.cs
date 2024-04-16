using Newtonsoft.Json;

namespace FashionGrid.UI.Models.Dtos
{
    public class CartResponseDto
    {
        [JsonProperty("result")]
        public CartDto Result { get; set; }

        [JsonProperty("isSuccess")]
        public bool IsSuccess { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }

}
