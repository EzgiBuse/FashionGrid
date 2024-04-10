using FashionGrid.ProductCatalogService.Models;

namespace FashionGrid.ProductCatalogService.Services.IServices
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(string id);
        Task CreateAsync(Product product);
        Task UpdateAsync(string id, Product product);
        Task DeleteAsync(string id);
    }
}
