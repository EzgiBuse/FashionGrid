using FashionGrid.UI.Models;
using FashionGrid.UI.Models.Dtos;
using FashionGrid.UI.Services.IServices;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;

namespace FashionGrid.UI.Controllers
{
    public class DealerPanelController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
       

        public DealerPanelController(IOrderService orderService, IProductService productService)
        {
            _orderService = orderService;
            _productService = productService;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                string dealerId = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
                string userName = User.FindFirst(JwtRegisteredClaimNames.Name)?.Value;  // If using JWT and name is included as a claim

                var response = await _orderService.GetDealerPanelIndexStatistics(dealerId);
                var statictics = JsonConvert.DeserializeObject<DealerPanelIndexViewModelResultDto>(response.Result.ToString());
                statictics.model.DealerName = userName;
                return View(statictics.model);
            }
            catch (Exception ex)
            {
                // Handle the exception as needed, log it, etc.
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }
    }
}
