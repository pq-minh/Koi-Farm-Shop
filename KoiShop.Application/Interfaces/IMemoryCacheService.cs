using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Interfaces
{
    public interface IMemoryCacheService
    {
        string CreateKey(string keyPrefix, string keySuffix);

        void SetCache(string key, object value, int? durationExpiration = null);

        Object? GetCache(string key);

        void RemoveCache(string key);

    }
}
