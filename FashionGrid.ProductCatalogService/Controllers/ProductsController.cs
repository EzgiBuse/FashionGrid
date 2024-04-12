using FashionGrid.ProductCatalogService.Models;
using FashionGrid.ProductCatalogService.Models.Dto;
using FashionGrid.ProductCatalogService.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace FashionGrid.ProductCatalogService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IDistributedCache _cache;
        private readonly ResponseDto _responseDto;

        public ProductsController(IProductService productService, IDistributedCache cache)
        {
            _productService = productService;
            _cache = cache;
            _responseDto = new ResponseDto();
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            string cacheKey = "productsList";
            try
            {
                var cachedData = await _cache.GetStringAsync(cacheKey);
                if (!string.IsNullOrEmpty(cachedData))
                {
                    _responseDto.Result = JsonSerializer.Deserialize<List<Product>>(cachedData);
                    _responseDto.Message = "Data retrieved from cache";
                }
                else
                {
                    var products = await _productService.GetAllAsync();
                    _responseDto.Result = products;
                    _responseDto.Message = "Data retrieved from database and cached";
                    var serializedProducts = JsonSerializer.Serialize(products);
                    await _cache.SetStringAsync(cacheKey, serializedProducts, new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                    });
                }
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                return BadRequest(_responseDto);
            }
            return Ok(_responseDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(string id)
        {
            string cacheKey = $"product_{id}";
            try
            {
                var cachedData = await _cache.GetStringAsync(cacheKey);
                if (!string.IsNullOrEmpty(cachedData))
                {
                    _responseDto.Result = JsonSerializer.Deserialize<Product>(cachedData);
                    _responseDto.Message = "Data retrieved from cache";
                }
                else
                {
                    var product = await _productService.GetByIdAsync(id);
                    if (product == null)
                    {
                        _responseDto.IsSuccess = false;
                        _responseDto.Message = "Product not found.";
                        _responseDto.Result = null;
                        return NotFound(_responseDto);
                    }
                    _responseDto.Result = product;
                    _responseDto.Message = "Data retrieved from database and cached";
                    var serializedProduct = JsonSerializer.Serialize(product);
                    await _cache.SetStringAsync(cacheKey, serializedProduct, new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                    });
                }
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                return BadRequest(_responseDto);
            }
            return Ok(_responseDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            try
            {
                await _productService.CreateAsync(product);
                _responseDto.Result = product;
                _responseDto.Message = "Product created successfully.";

                //remove the products cache
                await _cache.RemoveAsync("productsList");
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                return BadRequest(_responseDto);
            }
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, _responseDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(string id, [FromBody] Product productUpdate)
        {
            try
            {
                var productExists = await _productService.GetByIdAsync(id);
                if (productExists == null)
                {
                    _responseDto.IsSuccess = false;
                    _responseDto.Message = "Product not found.";
                    return NotFound(_responseDto);
                }
                await _productService.UpdateAsync(id, productUpdate);
                _responseDto.Result = productUpdate;
                _responseDto.Message = "Product updated successfully.";

                // invalidating the cache 
                string cacheKey = $"product_{id}";
                await _cache.RemoveAsync(cacheKey); 
                //remove the products cache
                await _cache.RemoveAsync("productsList");
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                return BadRequest(_responseDto);
            }
            return Ok(_responseDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            try
            {
                var product = await _productService.GetByIdAsync(id);
                if (product == null)
                {
                    _responseDto.IsSuccess = false;
                    _responseDto.Message = "Product not found.";
                    return NotFound(_responseDto);
                }
                await _productService.DeleteAsync(id);
                _responseDto.Message = "Product deleted successfully.";

                // invalidating the cache 
                string cacheKey = $"product_{id}";
                await _cache.RemoveAsync(cacheKey);
                //remove the products cache
                await _cache.RemoveAsync("productsList");
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                return BadRequest(_responseDto);
            }
            return Ok(_responseDto);
        }
    }
}
