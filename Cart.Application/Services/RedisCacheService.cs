using Cart.Application.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Cart.Application.Services
{    
    public class RedisCacheService : ICacheService
    {
        private readonly IConnectionMultiplexer _connectionMultiplexer;
        public RedisCacheService(IConnectionMultiplexer connectionMultiplexer)
        {
            _connectionMultiplexer = connectionMultiplexer;
        }

        public async Task<T> GetCacheValue<T>(string key)
        {
            var db = _connectionMultiplexer.GetDatabase();
            var data = await db.StringGetAsync(key);
            return data.HasValue ? JsonSerializer.Deserialize<T>(data) : default;
        }
        public async Task SetCacheValue<T>(string key, T value, TimeSpan expiryWindow)
        {
            var db = _connectionMultiplexer.GetDatabase();
            var data = JsonSerializer.Serialize(value);
            await db.StringSetAsync(key, data, expiryWindow);
        }
        public async Task<bool> DeleteCacheValue(string key)
        {
            var db = _connectionMultiplexer.GetDatabase();
            return await db.KeyDeleteAsync(key);
        }
    }
}
