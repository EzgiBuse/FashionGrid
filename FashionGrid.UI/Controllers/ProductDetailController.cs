using FashionGrid.UI.Models.Dtos;
using FashionGrid.UI.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FashionGrid.UI.Controllers
{
    public class ProductDetailController : Controller
    {
        private readonly IProductService _productService;

        public ProductDetailController(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IActionResult> Index(string id = null)
        {
            ResponseDto response = await _productService.GetProductByIdAsync(id);
            if (response.IsSuccess && response.Result != null)
            {
                // Deserialize it into ProductResponseDto
                var apiResponse = JsonConvert.DeserializeObject<ProductResponseDto>(response.Result.ToString());

                // Checking if the deserialization was successful and apiResponse is not null
                if (apiResponse != null)
                {
                    // Using apiResponse.Result which is the ProductDto
                    var product = apiResponse.Result;
                    var a = product.GetType();

                    return View(product);
                }
                else
                {
                    //the case where deserialization failed
                    return View(new ProductDto());
                }
            }
            else
            {
                // the case where response.IsSuccess is false or Result is null
                return View(new ProductDto());
            }
        }

      
    }
}
