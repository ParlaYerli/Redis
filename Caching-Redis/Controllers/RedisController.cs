using Caching_Redis.Caching;
using Caching_Redis.Caching.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Caching_Redis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class RedisController : ControllerBase
    {
        private readonly RedisCacheService _cacheService;

        public RedisController(RedisCacheService cacheService)
        {
            _cacheService = cacheService;
        }
        [HttpGet("cache/{key}")]
        public async Task<IActionResult> Get(string key)
        {
            return Ok(await _cacheService.GetValueAsync(key));
        }

        [HttpPost("cache")]
        public async Task<IActionResult> Post([FromBody] RedisCacheRequestModel model)
        {
            await _cacheService.SetValueAsync(model.Key, model.Value);
            return Ok();
        }
        [HttpDelete("cache/{key}")]
        public async Task<IActionResult> Delete(string key)
        {
            await _cacheService.Clear(key);
            return Ok();
        }
    }
}
