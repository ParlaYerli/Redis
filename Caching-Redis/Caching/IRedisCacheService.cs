namespace Caching_Redis.Caching
{
    public interface IRedisCacheService
    {
        Task<string> GetValueAsync(string key);
        Task<bool> SetValueAsync(string key, string value);
        //GetOrAdd fonksiyonu ile cache’den veri çektiğimizde veri null ise aynı zamanda set işlemini de yapıyoruz
        Task<T> GetOrAddAsync<T>(string key, Func<Task<T>> action) where T : class;
        T GetOrAdd<T>(string key, Func<T> action) where T : class;
        Task Clear(string key);
        void ClearAll();
    }
}
