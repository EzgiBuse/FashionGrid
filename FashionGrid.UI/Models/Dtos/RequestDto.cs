using System.Security.AccessControl;
using static FashionGrid.UI.Utilities.Standard;

namespace FashionGrid.UI.Models.Dtos
{
    public class RequestDto
    {
        public ApiType ApiType { get; set; } = ApiType.GET;
        public string Url { get; set; }
        public object Data { get; set; }
        public string AccessToken { get; set; }
    }
}
