using FashionGrid.UI.Models;
using FashionGrid.UI.Models.Dtos;
using FashionGrid.UI.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json;

namespace FashionGrid.UI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IActionResult> Index()
        {
            ResponseDto response = await _productService.GetAllProductsAsync();
            if (response.IsSuccess && response.Result != null)
            {
                // Deserialize it into ProductListResponseDto
                var apiResponse = JsonConvert.DeserializeObject<ProductListResponseDto>(response.Result.ToString());

                // Checking if the deserialization was successful and apiResponse is not null
                if (apiResponse != null)
                {
                    // Using apiResponse.Result which is the List<ProductDto>
                    var products = apiResponse.Result;
                    return View(products);
                }
                else
                {
                    //the case where deserialization failed
                    return View(new List<ProductDto>());
                }
            }
            else
            {
                // the case where response.IsSuccess is false or Result is null
                return View(new List<ProductDto>());
            }


        }

        //public async Task<IActionResult> ProductDetail1(string id)
        //{
        //    ResponseDto response = await _productService.GetProductByIdAsync(id);
        //    if (response.IsSuccess && response.Result != null)
        //    {
        //        // Deserialize it into ProductResponseDto
        //        var apiResponse = JsonConvert.DeserializeObject<ProductResponseDto>(response.Result.ToString());

        //        // Checking if the deserialization was successful and apiResponse is not null
        //        if (apiResponse != null)
        //        {
        //            // Using apiResponse.Result which is the ProductDto
        //            var product = apiResponse.Result;
        //            var a = product.GetType();
                    
        //            return View("ProductDetail",product);
        //        }
        //        else
        //        {
        //            //the case where deserialization failed
        //            return View(new ProductDto());
        //        }
        //    }
        //    else
        //    {
        //        // the case where response.IsSuccess is false or Result is null
        //        return View(new ProductDto());
        //    }
        //}
    }
}