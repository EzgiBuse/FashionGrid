using Newtonsoft.Json;

namespace FashionGrid.UI.Models.Dtos
{
    public class DealerPanelIndexViewModelResultDto
    {
        [JsonProperty("result")]
        public DealerPanelIndexViewModel model { get; set; }
    }
}
