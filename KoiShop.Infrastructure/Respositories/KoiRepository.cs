using KoiShop.Domain.Entities;
using KoiShop.Domain.Respositories;
using KoiShop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Infrastructure.Respositories
{
    internal class KoiRepository: IKoiRepository
    {
        private readonly KoiShopV1DbContext _KoiShopV1DbContext;
        public KoiRepository(KoiShopV1DbContext KoiShopV1DbContext)
        {
            _KoiShopV1DbContext = KoiShopV1DbContext;
        }
        public async Task<IEnumerable<Koi>> GetAllKoi()
        {
            var koi = await _KoiShopV1DbContext.Kois.ToListAsync();
            return koi;
        }
        
        public async Task<IEnumerable<Koi>> GetAllKois()
        {
            return await _KoiShopV1DbContext.Kois.ToListAsync();
        }
    }
}
