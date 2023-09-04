//using StackExchange.Redis;

//namespace Caching.DataAccess.Repository.Caching
//{
//    public interface IRedisCacheService
//    {
//        Task<string> GetValueAsync(string key);
//        Task<bool> SetValueAsync(string key, string value);
//        Task<string> HashGetAsync(string key);
//        Task<bool> HashSetAsync(string key, string value);
//        Task<bool> KeyExistsAsync(string key);
//        Task<HashEntry[]> HashGetAllAsync(string key); 
//        Task<T> GetOrAddAsync<T>(string key, Func<Task<T>> action) where T : class;
//        T GetOrAdd<T>(string key, Func<T> action) where T : class;
//        Task Clear(string key);
//        void ClearAll();
//    }
//}
