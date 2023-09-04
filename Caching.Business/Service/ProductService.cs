using Caching.Business.Service;
using Caching_Redis.DataAccess.Repository;
namespace Caching.Business.Product
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Data.Model.Product> CreateProductAsync(Data.Model.Product product)
        {
            await _productRepository.CreateProduct(product);
            
            return product;
        }

        public async Task<Data.Model.Product> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return product;
        }

        public async Task<List<Data.Model.Product>> GetProducts()
        {
            var list = await _productRepository.GetAsync();
            return list;
        }
    }
}
