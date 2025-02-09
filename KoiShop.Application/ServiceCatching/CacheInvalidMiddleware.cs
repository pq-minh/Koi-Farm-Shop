using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.ServiceCatching
{
    public class CacheInvalidMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly IMemoryCache _memoryCache;

        public CacheInvalidMiddleware (RequestDelegate requestDelegate, IMemoryCache memoryCache)
        {
            _requestDelegate = requestDelegate;
            _memoryCache = memoryCache;
        }

        // Check tyoe api to process
        public async Task Invoke (HttpContext context)
        {
            if (context.Request.Method == "POST" || context.Request.Method == "PUT" || context.Request.Method == "DELETE")
            {
                var cachKeyToRemove = new Dictionary<string, string>
                {
                    {"ListKoiOnSale", "Fish" },
                    { "Fish", "KoiOnSale"}
                };
                foreach (var paramkey in cachKeyToRemove)
                {
                    var param = context.Request.RouteValues[paramkey.Key]?.ToString();
                    if (param != null)
                    _memoryCache.Remove(paramkey.Key);
                }
            }

            // Có cái này để báo hiệu xử lí các middleware tiếp theo
            await _requestDelegate.Invoke (context);
        }
    }
}
