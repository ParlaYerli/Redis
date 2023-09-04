using StackExchange.Redis;
using System.Text.Json;

namespace Caching.DataAccess.Repository.Caching
{
    public class RedisCacheService
    {
        private readonly IConnectionMultiplexer _redisCon;
        private readonly IDatabase _cache;
        private TimeSpan ExpireTime => TimeSpan.FromDays(1);

        public RedisCacheService(IConnectionMultiplexer redisCon)
        {
            _redisCon = redisCon;
            _cache = redisCon.GetDatabase();
        }
       

        public IDatabase GetDb(int db)
        {
            return _redisCon.GetDatabase(db);
        }
        public async Task Clear(string key)
        {
            await _cache.KeyDeleteAsync(key);
        }
        
    }
}
