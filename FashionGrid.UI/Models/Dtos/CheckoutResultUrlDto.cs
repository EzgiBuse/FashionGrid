using Newtonsoft.Json;

namespace FashionGrid.UI.Models.Dtos
{
   
        public class CheckoutResultUrlDto
        {
            [JsonProperty("url")]
            public string Url { get; set; }
        }

    
}
