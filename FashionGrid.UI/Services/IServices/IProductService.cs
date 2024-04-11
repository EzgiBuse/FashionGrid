using FashionGrid.UI.Models;
using FashionGrid.UI.Models.Dtos;

namespace FashionGrid.UI.Services.IServices
{
   
        public interface IProductService
        {
            Task<ResponseDto?> GetAllProductsAsync();
            Task<ResponseDto?> GetProductByIdAsync(int id);
            Task<ResponseDto?> CreateProductAsync(Product product);
            Task<ResponseDto?> UpdateProductAsync(Product product);
            Task<ResponseDto?> DeleteProductAsync(int id);
        }
    
}
