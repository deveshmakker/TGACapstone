using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart.Application.Interfaces
{
    public interface ICacheService
    {
        public Task<T> GetCacheValue<T>(string key);
        public Task SetCacheValue<T>(string key, T value, TimeSpan expiryWindow);        
        public Task<bool> DeleteCacheValue(string key);
    }
}
