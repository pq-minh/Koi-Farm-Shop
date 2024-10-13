using KoiShop.Domain.Entities;
using KoiShop.Domain.Respositories;
using KoiShop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dropbox.Api.Files.SearchMatchType;

namespace KoiShop.Infrastructure.Respositories
{
    public class KoiRepository : IKoiRepository
    {
        private readonly KoiShopV1DbContext _KoiShopV1DbContext;
        public KoiRepository(KoiShopV1DbContext KoiShopV1DbContext)
        {
            _KoiShopV1DbContext = KoiShopV1DbContext;
        }

        // Koi Methods ============================================================================================
        public async Task<IEnumerable<Koi>> GetAllKoiAsync()
        {
            return await _KoiShopV1DbContext.Kois.ToListAsync();
        }
        public async Task<Koi> AddKoiAsync(Koi koi)
        {
            _KoiShopV1DbContext.Kois.Add(koi);
            await _KoiShopV1DbContext.SaveChangesAsync();
            return koi;
        }
        public async Task<Koi> GetKoiByIdAsync(int id)
        {
            return await _KoiShopV1DbContext.Kois.FindAsync(id);
        }

        public async Task<Koi> UpdateKoiAsync(Koi koi)
        {
            _KoiShopV1DbContext.Kois.Update(koi);
            await _KoiShopV1DbContext.SaveChangesAsync();
            return koi;
        }

        // KoiCategory Methods =====================================================================================
        public async Task<IEnumerable<KoiCategory>> GetAllKoiCategoryAsync()
        {
            return await _KoiShopV1DbContext.KoiCategories.ToListAsync();
        }

        public async Task<KoiCategory> GetKoiCategoryByIdAsync(int id)
        {
            return await _KoiShopV1DbContext.KoiCategories.FindAsync(id);
        }

        public async Task<KoiCategory> AddKoiCategoryAsync(KoiCategory koiCategory)
        {
            _KoiShopV1DbContext.KoiCategories.Add(koiCategory);
            await _KoiShopV1DbContext.SaveChangesAsync();
            return koiCategory;
        }

        public async Task<KoiCategory> UpdateKoiCategoryAsync(KoiCategory koiCategory)
        {
            _KoiShopV1DbContext.KoiCategories.Update(koiCategory);
            await _KoiShopV1DbContext.SaveChangesAsync();
            return koiCategory;
        }

        public async Task<bool> DeleteKoiCategoryAsync(int fishTypeId)
        {
            var koiCategory = await _KoiShopV1DbContext.KoiCategories.FindAsync(fishTypeId);

            if (koiCategory == null)
                return false;

            _KoiShopV1DbContext.KoiCategories.Remove(koiCategory);
            await _KoiShopV1DbContext.SaveChangesAsync(); 

            return true; 
        }
        public async Task<List<Koi>> GetKoiInKoiCategoryAsync(int fishTypeId)
        {
            return await _KoiShopV1DbContext.Kois.Where(koi => koi.FishTypeId == fishTypeId).ToListAsync();
        }

    }
}
