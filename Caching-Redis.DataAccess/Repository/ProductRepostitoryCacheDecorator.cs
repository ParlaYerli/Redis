using Caching.Data.Model;
using Caching.DataAccess.Repository.Caching;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Security;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Caching_Redis.DataAccess.Repository
{
    public class ProductRepostitoryCacheDecorator : IProductRepository
    {
        private const string ProductKey = "products";
        private readonly IProductRepository _repository;
        private readonly IDatabase _cacheDatabase;
        public ProductRepostitoryCacheDecorator(IProductRepository repository, RedisCacheService redisCacheService)
        {
            _repository = repository;
            _cacheDatabase = redisCacheService.GetDb(0);
        }

        public async Task<Product> CreateProduct(Product product)
        {
            var newroduct = _repository.CreateProduct(product);
            var existingData = await _cacheDatabase.HashGetAsync(ProductKey, product.ProductId.ToString());

            if (existingData.IsNull)
            {
                // Mevcut veri yok, yeni veriyi ekliyorum
                await _cacheDatabase.HashSetAsync(ProductKey, product.ProductId, JsonSerializer.Serialize(product));
            }
            else
            {
                // Mevcut veri varsa, üzerine yazıyorum
                await _cacheDatabase.HashSetAsync(ProductKey, product.ProductId, JsonSerializer.Serialize(product));
            }
            return product;
        }

        public async Task<List<Product>> GetAsync()
        {
            List<Product> products = new();

            if (_cacheDatabase.KeyExists(ProductKey))
            {
                foreach (var item in (await _cacheDatabase.HashGetAllAsync(ProductKey)).ToList())
                {
                    var product = JsonSerializer.Deserialize<Product>(item.Value);
                    products.Add(product!);
                }

                return products;
            }

            return await LoadCacheFromDb();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            if (_cacheDatabase.KeyExists(ProductKey))
            {
                var product = await _cacheDatabase.HashGetAsync(ProductKey, id);
                return product.HasValue ? JsonSerializer.Deserialize<Product>(product) : null;
            }

            return (await LoadCacheFromDb()).FirstOrDefault(x => x.ProductId == id);
        }


        private async Task<List<Product>> LoadCacheFromDb()
        {
            var products = await _repository.GetAsync();
            products.ForEach(p => { _cacheDatabase.HashSetAsync(ProductKey, p.ProductId, JsonSerializer.Serialize(p)); });

            return products;
        }
    }
}
