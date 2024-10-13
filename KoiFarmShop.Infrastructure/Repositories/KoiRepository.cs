using KoiFarmShop.Domain.Entities;
using KoiFarmShop.Domain.Interfaces;
using KoiFarmShop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Infrastructure.Repositories
{
    public class KoiRepository : IKoiRepository
    {
        private readonly KoiFarmShopContext _context;

        public KoiRepository(KoiFarmShopContext context)
        {
            _context = context;
        }

        // Koi Methods ============================================================================================
        public async Task<IEnumerable<Koi>> GetAllKoiAsync()
        {
            return await _context.Kois.ToListAsync();
        }
        public async Task<Koi> AddKoiAsync(Koi koi)
        {
            _context.Kois.Add(koi);
            await _context.SaveChangesAsync();
            return koi;
        }
        public async Task<Koi> GetKoiByIdAsync(int id)
        {
            return await _context.Kois.FindAsync(id);
        }

        public async Task<Koi> UpdateKoiAsync(Koi koi)
        {
            _context.Kois.Update(koi);
            await _context.SaveChangesAsync();
            return koi;
        }

        //public async Task<bool> DeleteKoiAsync(int id)
        //{
        //    var koi = await _context.Kois.FindAsync(id);
        //    if (koi == null) return false;

        //    _context.Kois.Remove(koi);
        //    await _context.SaveChangesAsync();
        //    return true;
        //}


        // KoiCategory Methods =====================================================================================
        public async Task<IEnumerable<KoiCategory>> GetAllKoiCategoryAsync()
        {
            return await _context.KoiCategories.ToListAsync();
        }

        public async Task<KoiCategory> GetKoiCategoryByIdAsync(int id)
        {
            return await _context.KoiCategories.FindAsync(id);
        }

        public async Task<KoiCategory> AddKoiCategoryAsync(KoiCategory koiCategory)
        {
            _context.KoiCategories.Add(koiCategory);
            await _context.SaveChangesAsync();
            return koiCategory;
        }

        public async Task<KoiCategory> UpdateKoiCategoryAsync(KoiCategory koiCategory)
        {
            _context.KoiCategories.Update(koiCategory);
            await _context.SaveChangesAsync();
            return koiCategory;
        }

        public async Task<bool> DeleteKoiCategoryAsync(int fishTypeId)
        {
            // Tìm thể loại Koi với FishTypeId
            var koiCategory = await _context.KoiCategories.FindAsync(fishTypeId);

            // Nếu không tìm thấy thể loại, trả về false
            if (koiCategory == null)
                return false;

            // Xóa thể loại Koi
            _context.KoiCategories.Remove(koiCategory);
            await _context.SaveChangesAsync(); // Lưu thay đổi vào DB

            return true; // Trả về true nếu xóa thành công
        }


        public async Task<List<Koi>> GetKoiInKoiCategoryAsync(int fishTypeId)
        {
            return await _context.Kois.Where(koi => koi.FishTypeId == fishTypeId).ToListAsync();
        }

    }
}
