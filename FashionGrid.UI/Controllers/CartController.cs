using Microsoft.AspNetCore.Mvc;

namespace FashionGrid.UI.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
