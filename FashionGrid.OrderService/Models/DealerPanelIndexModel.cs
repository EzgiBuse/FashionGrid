using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace FashionGrid.OrderService.Models
{
    public class DealerPanelIndexModel
    {
        public string DealerName { get; set; } = "";
        public int OrderCountDaily { get; set; }
        public decimal OrderTotalDaily { get; set; }
        public int OrderCountWeekly { get; set; }
        public decimal OrderTotalWeekly { get; set; }
        public int OrderCountMonthly { get; set; }
        public decimal OrderTotalMonthly { get; set; }
        public int TotalCustomerCount { get; set; }
        public int OrderCountTotal { get; set; }
        public string TopProductName { get; set; }
        public decimal OrderTotal { get; set;}


    }
}
