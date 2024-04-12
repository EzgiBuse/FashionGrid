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
            try
            {
                return await _products.Find(product => true).ToListAsync();
            }
            catch (Exception ex)
            {
               // _logger.LogError("An error occurred when retrieving all products: {ExceptionMessage}", ex.Message);
                throw;
            }
        }

        public async Task<Product> GetByIdAsync(string id)
        {
            try
            {
                return await _products.Find(product => product.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
               // _logger.LogError("An error occurred when retrieving a product by ID: {ExceptionMessage}", ex.Message);
                throw;
            }
        }

        public async Task CreateAsync(Product product)
        {
            try
            {
                await _products.InsertOneAsync(product);
            }
            catch (Exception ex)
            {
               // _logger.LogError("An error occurred when creating a new product: {ExceptionMessage}", ex.Message);
                throw;
            }
        }

        public async Task UpdateAsync(string id, Product product)
        {
            try
            {
                await _products.ReplaceOneAsync(prod => prod.Id == id, product);
            }
            catch (Exception ex)
            {
               // _logger.LogError("An error occurred when updating a product: {ExceptionMessage}", ex.Message);
                throw;
            }
        }

        public async Task DeleteAsync(string id)
        {
            try
            {
                await _products.DeleteOneAsync(product => product.Id == id);
            }
            catch (Exception ex)
            {
               // _logger.LogError("An error occurred when deleting a product: {ExceptionMessage}", ex.Message);
                throw;
            }
        }
    }
}
