using KoiShop.Application.Dtos;
using KoiShop.Application.Interfaces;
using KoiShop.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.ServiceCatching
{
    public class FishCachingService : IFishCachingService
    {
        private readonly IMemoryCacheService _memoryCache;
        private readonly IKoiService _koiService;

        public FishCachingService (IMemoryCacheService memoryCache, IKoiService koiService)
        {
            _koiService = koiService;
            _memoryCache = memoryCache;
        }

        public async Task<IEnumerable<KoiDto>> GetFishOnSale()
        {
            var cacheKey = _memoryCache.CreateKey("Fish", "ListKoiOnSale");
            var koi = _memoryCache.GetCache(cacheKey);

            if (koi == null)
            {
                koi = await _koiService.GetAllKoi();
                _memoryCache.SetCache(cacheKey, koi);
            }

            return (IEnumerable<KoiDto>)koi;
        }

        public async Task<KoiDto> GetFishIdOnsale(int id)
        {
            var cacheKey = _memoryCache.CreateKey("Fish", "KoiOnSale");
            var koi = _memoryCache.GetCache(cacheKey);

            if (koi == null)
            {
                koi = await _koiService.GetKoi(id);
                _memoryCache.SetCache(cacheKey, koi);
            }

            return (KoiDto)koi;
        }
    }
}
