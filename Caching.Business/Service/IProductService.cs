using Caching.Data.Model;
namespace Caching.Business.Service
{
    public interface IProductService
    {
        Task<List<Data.Model.Product>> GetProducts();

        Task<Data.Model.Product> GetProductByIdAsync(int id);
        Task<Data.Model.Product> CreateProductAsync(Data.Model.Product product);
    }
}
