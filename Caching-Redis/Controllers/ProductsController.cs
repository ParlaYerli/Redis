using Caching.Business.Service;
using Caching.Data.Model;
using Caching.DataAccess.Repository.Caching;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Caching_Redis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly RedisCacheService _redisService;
        public ProductsController(IProductService productService, RedisCacheService redisService)
        {
            _productService = productService;
            _redisService = redisService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _productService.GetProducts());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _productService.GetProductByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            await _productService.CreateProductAsync(product);
            return Ok( await _productService.CreateProductAsync(product));
        }

    }
}
