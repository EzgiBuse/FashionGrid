using FashionGrid.UI.Models;
using FashionGrid.UI.Models.Dtos;
using FashionGrid.UI.Services.IServices;
using static FashionGrid.UI.Utilities.Standard;

namespace FashionGrid.UI.Services.Services
{
   
        public class ProductService : IProductService
        {
            private readonly IBaseService _baseService;
            public ProductService(IBaseService baseService)
            {
                _baseService = baseService;
            }

            public async Task<ResponseDto?> CreateProductAsync(Product product)
            {
                return await _baseService.SendAsync(new RequestDto()
                {
                    ApiType = ApiType.POST,
                    Data = product,
                    Url = "https://localhost:7240/Products"
                });
            }

            public async Task<ResponseDto?> DeleteProductAsync(string id)
            {
                return await _baseService.SendAsync(new RequestDto()
                {
                    ApiType = ApiType.DELETE,
                    Url = "https://localhost:7240/Products/" + id
                });
            }

            public async Task<ResponseDto?> GetAllProductsAsync()
            {
                return await _baseService.SendAsync(new RequestDto()
                {
                    ApiType = ApiType.GET,
                    Url = "https://localhost:7240/Products"
                }, withBearer: false);
            }
       

            public async Task<ResponseDto?> GetProductByIdAsync(string id)
            {
                return await _baseService.SendAsync(new RequestDto()
                {
                    ApiType = ApiType.GET,
                    Url = "https://localhost:7240/Products/" + id
                }, withBearer: false);
             }

            public async Task<ResponseDto?> UpdateProductAsync(Product product)
            {
                return await _baseService.SendAsync(new RequestDto()
                {
                    ApiType = ApiType.PUT,
                    Data = product,
                    Url = "https://localhost:7240/Products"
                }, withBearer: false);
            }
        }
    
}
