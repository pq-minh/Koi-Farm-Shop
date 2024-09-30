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
    internal class KoiRepository(KoiShopV1DbContext koiShopV1DbContext) : IKoiRepository
    {
        public async Task<IEnumerable<Koi>> GetAllKoi()
        {
            var koi = await koiShopV1DbContext.Kois.ToListAsync();
            return koi;
        }
    }
}
