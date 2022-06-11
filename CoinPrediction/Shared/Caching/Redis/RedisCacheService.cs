using Newtonsoft.Json;

namespace Shared.Caching.Redis
{
    public class RedisCacheService : ICacheService
    {
        private readonly RedisServer _redisServer;

        public RedisCacheService(RedisServer redisServer)
        {
            _redisServer = redisServer;
        }

        public async Task Add(string key, object data,DateTime? expires = null)
        {
            if (Any(key).Result)
                return;

            string jsonData = JsonConvert.SerializeObject(data);
            TimeSpan expiryTimeSpan;

            if (expires!=null && expires.Value!=default)
                expiryTimeSpan = expires.Value.Subtract(DateTime.UtcNow);
            else
            {
                expires=DateTime.UtcNow.AddDays(1);
                expiryTimeSpan = expires.Value.Subtract(DateTime.UtcNow);
            }

            await _redisServer.Database.StringSetAsync(key, jsonData, expiryTimeSpan);
        }

        //Key-Value olarak data atama
        public async Task Add(string key, string name, object data)
        {
            if (Any(key,name).Result)
                return;

            string jsonData = JsonConvert.SerializeObject(data);
            await _redisServer.Database.HashSetAsync(key, name, jsonData);
        }
        
        //Mevcut datanın üzerine ekleme
        public async Task Append(string key, object data)
        {
            if (!Any(key).Result)
                return;

            string jsonData = JsonConvert.SerializeObject(data);
            await _redisServer.Database.StringAppendAsync(key, jsonData);
        }

        //Mevcut array datanın en başına ekleme 
        public async Task LPush(string key, object data)
        {
            if (!Any(key).Result)
                return;

            string jsonData = JsonConvert.SerializeObject(data);
            await _redisServer.Database.ListLeftPushAsync(key, jsonData);
        }

        //Mevcut array datanın en sonuna ekleme 
        public async Task RPush(string key, object data)
        {
            if (!Any(key).Result)
                return;

            string jsonData = JsonConvert.SerializeObject(data);
            await _redisServer.Database.ListRightPushAsync(key, jsonData);
        }
        
        public Task<bool> Any(string key, string name)
        {
            return _redisServer.Database.HashExistsAsync(key, name);
        }

        public Task<bool> Any(string key)
        {
            return _redisServer.Database.KeyExistsAsync(key);
        }

        public async Task<T?> Get<T>(string key)
        {
            if (Any(key).Result)
            {
                string jsonData = await _redisServer.Database.StringGetAsync(key);
                return JsonConvert.DeserializeObject<T>(jsonData);
            }

            return default;
        }

        public async Task<object?> Get(string key)
        {
            if (Any(key).Result)
            {
                string jsonData = await _redisServer.Database.StringGetAsync(key);
                return JsonConvert.DeserializeObject<object>(jsonData);
            }

            return default;
        }

        //Key-Value
        public async Task<T?> Get<T>(string key, string name)
        {
            if (Any(key, name).Result)
            {
                string jsonData = await _redisServer.Database.HashGetAsync(key, name);
                return JsonConvert.DeserializeObject<T>(jsonData);
            }

            return default;
        }

        //Key-Value
        public async Task Remove(string key, string name)
        {
            if (!Any(key, name).Result)
                return;

            await _redisServer.Database.HashDeleteAsync(key, name);
        }

        public async Task Remove(string key)
        {
            if (!Any(key).Result)
                return;

            await _redisServer.Database.KeyDeleteAsync(key);
        }

        //Cachde db deki tüm verileri siler
        public void Clear()
        {
            _redisServer.FlushDatabase();
        }
    }
}
