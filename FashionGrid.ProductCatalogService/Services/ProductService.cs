using FashionGrid.ProductCatalogService.Models;
using FashionGrid.ProductCatalogService.Services.IServices;
using MongoDB.Driver;

namespace FashionGrid.ProductCatalogService.Services
{
    public class ProductService : IProductService
    {
        private readonly IMongoCollection<Product> _products;

        public ProductService(IMongoClient mongoClient, IConfiguration configuration)
        {
            var databaseName = configuration.GetValue<string>("MongoDB:DatabaseName");
            var collectionName = configuration.GetValue<string>("MongoDB:CollectionName"); 
            var database = mongoClient.GetDatabase(databaseName);
            _products = database.GetCollection<Product>(collectionName);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _products.Find(product => true).ToListAsync();
        }

        public async Task<Product> GetByIdAsync(string id)
        {
            return await _products.Find(product => product.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Product product)
        {
            await _products.InsertOneAsync(product);
        }

        public async Task UpdateAsync(string id, Product product)
        {
            await _products.ReplaceOneAsync(prod => prod.Id == id, product);
        }

        public async Task DeleteAsync(string id)
        {
            await _products.DeleteOneAsync(product => product.Id == id);
        }
    }
}
