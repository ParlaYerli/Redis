

using Caching.Data.Model;

namespace Caching_Redis.DataAccess.Repository
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAsync();
        Task<Product> GetByIdAsync(int id);
        Task<Product> CreateProduct(Product model);
    }
}
