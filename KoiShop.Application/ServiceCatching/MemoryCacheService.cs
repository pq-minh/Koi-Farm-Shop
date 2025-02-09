using KoiShop.Application.Interfaces;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.RateLimiting;
using System.Threading.Tasks;

namespace KoiShop.Application.ServiceCatching
{
    public class MemoryCacheService : IMemoryCacheService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly TimeSpan _defaultExpiration = TimeSpan.FromMinutes(5);

        public MemoryCacheService (IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public string CreateKey (string keyPrefix, string keySuffix)
        {
            return $"{keyPrefix}_{keySuffix}";
        }

        public void SetCache (string key, object value, int? durationExpiration = null)
        {
            var cacheOption = new MemoryCacheEntryOptions {

                //AbsoluteExpiration: set thời gian hết hạn cố định 
                //SlidingExpiration: set thời gian hết hạn tương đối. Mỗi khi truy cập lại thì thời gian hết hạn sẽ reset lại
                SlidingExpiration = durationExpiration.HasValue ? TimeSpan.FromMinutes(durationExpiration.Value) : _defaultExpiration,
                Size = 1024
            };

            _memoryCache.Set(key, value, cacheOption);
        }

        public Object? GetCache (string key)
        {
            //TryGetValue kiểm tra cache với key đó có value hay không có -> value = true và ngược lại
            return _memoryCache.TryGetValue(key, out var value) ? value: null;
        }

        public void RemoveCache (string key)
        {
            _memoryCache.Remove(key);
        }

    }
}
