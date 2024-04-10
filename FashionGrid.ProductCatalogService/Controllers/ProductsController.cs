using FashionGrid.ProductCatalogService.Models;
using FashionGrid.ProductCatalogService.Models.Dto;
using FashionGrid.ProductCatalogService.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FashionGrid.ProductCatalogService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private ResponseDto _responseDto;

        public ProductsController(IProductService productService, ResponseDto responseDto)
        {
            _productService = productService;
            _responseDto = responseDto;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productService.GetAllAsync();
            return Ok(products);
        }

        // GET: api/Products/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(string id)
        {
            var product = await _productService.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // POST: api/Products
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            await _productService.CreateAsync(product);
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        // PUT: api/Products/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(string id, [FromBody] Product productUpdate)
        {
            var product = await _productService.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            await _productService.UpdateAsync(id, productUpdate);

            return NoContent(); // Or return the updated product if preferred
        }

        // DELETE: api/Products/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var product = await _productService.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            await _productService.DeleteAsync(id);

            return NoContent();
        }
    }
}
