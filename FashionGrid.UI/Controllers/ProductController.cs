using Microsoft.AspNetCore.Mvc;

namespace FashionGrid.UI.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
