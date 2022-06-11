using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace Shared.Caching.Redis
{
    public class RedisServer
    {
        private readonly ConnectionMultiplexer _connectionMultiplexer;
        private IDatabase _database;
        private string _configurationString = string.Empty;
        private int _currentDatabaseId = default;

        public RedisServer(IConfiguration configuration)
        {
            CreateRedisConfigurationString(configuration);

            _connectionMultiplexer = ConnectionMultiplexer.Connect(_configurationString);
            _database = _connectionMultiplexer.GetDatabase(_currentDatabaseId);
        }

        public IDatabase Database => _database;

        public async void FlushDatabase()
        {
            await _connectionMultiplexer.GetServer(_configurationString).FlushDatabaseAsync(_currentDatabaseId);
        }

        public async void FlushAllDatabase()
        {
            await _connectionMultiplexer.GetServer(_configurationString).FlushAllDatabasesAsync();
        }

        private void CreateRedisConfigurationString(IConfiguration configuration)
        {
            _configurationString = configuration.GetConnectionString("Redis");
        }
    }
}