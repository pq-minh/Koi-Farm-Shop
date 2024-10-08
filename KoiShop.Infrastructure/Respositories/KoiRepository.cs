using KoiShop.Domain.Entities;
using KoiShop.Domain.Respositories;
using KoiShop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace KoiShop.Infrastructure.Respositories
{
    internal class KoiRepository : IKoiRepository
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

        public async Task<IEnumerable<Koi>> GetKoiWithCondition(string koiName, double? from, double? to, string sortBy, int pageNumber, int pageSize)
        {
            var allKoi = _KoiShopV1DbContext.Kois.AsQueryable();
            #region Filtering
            if (!string.IsNullOrEmpty(koiName))
            {
                allKoi = allKoi.Where(k => k.Name.ToLower().Contains(koiName.ToLower()));
            }
            if (from.HasValue)
            {
                allKoi = allKoi.Where(k => k.Price >= from);
            }
            if (to.HasValue)
            {
                allKoi = allKoi.Where(k => k.Price <= to);
            }
            #endregion
            #region Sorting
            //Default sort asc by name
            allKoi = allKoi.OrderBy(k => k.Name);
            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "name_desc": allKoi.OrderByDescending(k => k.Name); break;
                    case "price_asc": allKoi.OrderBy(k => k.Price); break;
                    case "price_desc": allKoi.OrderByDescending(k => k.Price); break;
                    default: allKoi.OrderBy(k => k.Name); break;
                }
            }
            #endregion

            #region Paging
            if (pageNumber <= 0)
            {
                pageNumber = 1;
            }
            if (pageSize <= 0)
            {
                pageSize = 5;
            }
            allKoi = allKoi.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            #endregion

            return await allKoi.ToListAsync();
        }
    }
}
