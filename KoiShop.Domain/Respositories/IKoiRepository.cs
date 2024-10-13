using KoiShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Domain.Respositories
{
   public interface IKoiRepository
    {
        // Koi Methods ============================================================================================
        Task<IEnumerable<Koi>> GetAllKoiAsync();
        Task<Koi> AddKoiAsync(Koi koi);
        Task<Koi> GetKoiByIdAsync(int id);
        Task<Koi> UpdateKoiAsync(Koi koi);

        //Task<bool> DeleteKoiAsync(int id);

        // KoiCategory Methods  =====================================================================================
        Task<IEnumerable<KoiCategory>> GetAllKoiCategoryAsync();
        Task<KoiCategory> GetKoiCategoryByIdAsync(int id);
        Task<KoiCategory> AddKoiCategoryAsync(KoiCategory koiCategory);
        Task<KoiCategory> UpdateKoiCategoryAsync(KoiCategory koiCategory);
        Task<bool> DeleteKoiCategoryAsync(int id);
        Task<List<Koi>> GetKoiInKoiCategoryAsync(int fishTypeId);
    }
}
