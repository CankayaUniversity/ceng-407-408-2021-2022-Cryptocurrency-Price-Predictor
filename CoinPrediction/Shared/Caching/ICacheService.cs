namespace Shared.Caching
{
    public interface ICacheService
    {
        Task<T?> Get<T>(string key);
        Task<object?> Get(string key);
        Task<T?> Get<T>(string key, string name);
        Task Add(string key, object data, DateTime? expires = null);
        Task Add(string key, string name, object data);
        Task Append(string key, object data);
        Task LPush(string key, object data);
        Task RPush(string key, object data);
        Task Remove(string key);
        Task Remove(string key, string name);
        void Clear();
        Task<bool> Any(string key);
        Task<bool> Any(string key, string name);
    }
}
