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
            return await _KoiShopV1DbContext.Kois.Where(k => k.Status == "OnSale").Include(k => k.FishType).ToListAsync();
        }

        public async Task<Koi> GetKoi (int id)   
        {
            return await _KoiShopV1DbContext.Kois.Where(k => k.KoiId == id && k.Status == "OnSale").Include(k => k.FishType).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Koi>> GetKoiWithCondition(string koiName, string typeFish, double? from, double? to, string sortBy, int pageNumber, int pageSize)
        {
            var allKoi = _KoiShopV1DbContext.Kois.Where(k => k.Status == "OnSale").Include(k => k.FishType).AsQueryable();
            #region Filtering
            if (!string.IsNullOrEmpty(koiName))
            {
                allKoi = allKoi.Where(k => k.Name.ToLower().Contains(koiName.ToLower()));
            }
            if (!string.IsNullOrEmpty(typeFish))
            {
                allKoi = allKoi.Where(k => k.FishType.TypeFish.ToLower().Contains(typeFish.ToLower()));
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
                    case "name_desc": allKoi = allKoi.OrderByDescending(k => k.Name); break;
                    case "price_asc": allKoi = allKoi.OrderBy(k => k.Price); break;
                    case "price_desc": allKoi = allKoi.OrderByDescending(k => k.Price); break;
                    default: allKoi = allKoi.OrderBy(k => k.Name); break;
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

        // Koi Methods ============================================================================================
        public async Task<IEnumerable<Koi>> GetKois()
        {
            return await _KoiShopV1DbContext.Kois.ToListAsync();
        }
        public async Task<bool> AddKoi(Koi koi)
        {
            _KoiShopV1DbContext.Kois.Add(koi);
            var result = await _KoiShopV1DbContext.SaveChangesAsync();
            return result > 0;
        }
        public async Task<Koi> GetKoiById(int id)
        {
            return await _KoiShopV1DbContext.Kois.FindAsync(id);
        }

        public async Task<bool> UpdateKoi(Koi koi)
        {
            _KoiShopV1DbContext.Kois.Update(koi);
            var result = await _KoiShopV1DbContext.SaveChangesAsync();
            return result > 0;
        }

        // KoiCategory Methods =====================================================================================
        public async Task<IEnumerable<KoiCategory>> GetAllKoiCategories()
        {
            return await _KoiShopV1DbContext.KoiCategories.ToListAsync();
        }
        public async Task<KoiCategory> GetKoiCategoryById(int id)
        {
            return await _KoiShopV1DbContext.KoiCategories.FindAsync(id);
        }
        public async Task<List<Koi>> GetKoisInKoiCategory(int fishTypeId)
        {
            return await _KoiShopV1DbContext.Kois.Where(koi => koi.FishTypeId == fishTypeId).ToListAsync();
        }


    }
}
